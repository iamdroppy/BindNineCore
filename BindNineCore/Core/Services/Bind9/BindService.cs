using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction;
using BindNineCore.Config;
using BindNineCore.Core.Database;
using BindNineCore.Core.Database.Entities;
using DotLiquid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Bind9
{
    internal class BindService : IBindApplySettings, IDisposable, IAsyncDisposable
    {
        private readonly ILogger<BindService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly BindConfig _cfg;

        public BindService(ILogger<BindService> logger, ApplicationDbContext dbContext, BindConfig cfg)
        {
            _logger = logger;
            _dbContext = dbContext;
            _cfg = cfg;
        }

        public async Task SaveAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            
            _logger.LogTrace("Starting to write settings...");
            try
            {
                await using StreamWriter writer = new(GetNamedConfigStream()) {AutoFlush = false};

                var domains = _dbContext.Domains.Include(s => s.Dns);
                foreach (var domain in domains)
                {
                    token.ThrowIfCancellationRequested();
                    await writer.WriteLineAsync(await GetLiquidAsync("named.template", domain, token));
                    await WriteZoneAsync(domain, Path.Join(_cfg.BindPath, "zones", "db." + domain.Domain), token);
                }

                token.ThrowIfCancellationRequested();
                await writer.FlushAsync();
                await RunPostBuild();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        private async Task WriteZoneAsync(DomainEntity domain, string path, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await using StreamWriter writer = new(new FileStream(path, FileMode.Create, FileAccess.Write)) 
                { AutoFlush = false};
            
            await writer.WriteLineAsync(await GetLiquidAsync("zone.template", domain, token));
            // write NS records
            
            // ReSharper disable once HeapView.ObjectAllocation.Possible
            // foreach (var dns in domain.Dns.OrderBy(s=>s.RecordType == RecordType.Nameserver))
            //     await WriteRecordAsync(writer, domain, dns);
            
            token.ThrowIfCancellationRequested();
            await writer.FlushAsync();
        }

        private Task WriteRecordAsync(StreamWriter writer, DomainEntity domain, DnsEntity dns)
        {
            return writer.WriteLineAsync(
                $"{dns.Subdomain}.{domain.Domain}.          IN      {dns.RecordType.GetShortName()}       {dns.Value}");
        }
        private Stream GetNamedConfigStream()
        {
            FileInfo namedConfigLocal = new (Path.Join(_cfg.BindPath, "named.conf.local"));
            return !namedConfigLocal.Exists 
                ? namedConfigLocal.Create()
                : new FileStream(namedConfigLocal.FullName, FileMode.Create, FileAccess.Write);
        }

        private async Task RunPostBuild()
        {
            if (_cfg.PostBuild is not { } builds) return;
            _logger.LogInformation($"Running {builds.Length} post-builds...");
            foreach (var b in builds)
            {
                try
                {
                    ProcessStartInfo psi = new()
                    {
                        CreateNoWindow = true,
                        FileName = b.Bin,
                        Arguments = b.Args
                    };
                    Process proc = new Process() {StartInfo = psi};
                    proc.Start();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message, ex);
                }
            }
            _logger.LogInformation($"Post-builds done.");
            
        }
        
        private Hash GetLiquidEntity(DomainEntity domain)
        {
            return Hash.FromAnonymousObject(
                new
                {
                    domain = domain.Domain,
                    dns_records = domain.Dns.Select(s => Hash.FromAnonymousObject(
                        new
                        {
                            subdomain = s.Subdomain,
                            record_type = s.ZoneRecordType,
                            value = s.Value
                        })).ToArray(),
                    save_path = Path.Join(_cfg.BindPath, "zones", "db." + domain.Domain),
                    cfg_save_path = Path.Join(_cfg.BindPathConfigOverride ?? _cfg.BindPath, "zones", "db." + domain.Domain).Replace("\\", "/")
                });
        }
        
        private async Task<string> GetLiquidAsync(string templateFile, DomainEntity domain, CancellationToken token = default)
        {
            Template template = Template.Parse(await File.ReadAllTextAsync(templateFile, token));  // Parses and compiles the template
            return template.Render(GetLiquidEntity(domain));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}