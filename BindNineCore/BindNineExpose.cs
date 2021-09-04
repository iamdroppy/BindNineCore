using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Dns;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Config;
using BindNineCore.Core;
using BindNineCore.Core.Database;
using BindNineCore.Core.Services.Bind9;
using BindNineCore.Core.Services.Dns;
using BindNineCore.Core.Services.Domain;
using BindNineCore.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BindNineCore
{
    public static class BindNineExpose
    {
        public static IServiceCollection AddBindNine(this IServiceCollection services, [NotNull] BindConfig config)
        {
            if (Equals(config, null))
                throw new ArgumentNullException(nameof(config));

            if (!Directory.Exists(config.BindPath))
                throw new BindDirectoryNotFoundException(config.BindPath);

            // create test file and delete.

            var rnd = new Random();
            var path = Path.Combine(config.BindPath, "tmp_" + rnd.Next(0, 999999) + ".txt");
            try
            {
                File.WriteAllText(path, "WRITE_TEST");
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw new BindDirectoryDeniedException(path, ex);
            }
            
            return services
                .AddSingleton(config)
                
                // starts services
                .AddScoped<IBindApplySettings, BindService>()
                
                .AddScoped<IDomainFetch, DomainFetch>()
                .AddScoped<IDomainDelete, DomainDelete>()
                .AddScoped<IDomainCreate, DomainCreate>()
                
                .AddScoped<IDnsCreate, DnsCreate>()
                .AddScoped<IDnsDelete, DnsDelete>()
                .AddScoped<IDnsFetch, DnsFetch>()
                
                .AddDbContext<ApplicationDbContext>(
                    c=>c.UseMySql(config.ConnectionString, ServerVersion.AutoDetect(config.ConnectionString)), 
                    ServiceLifetime.Scoped);
        }

        public static IServiceProvider UseBindNine(this IServiceProvider useBindNine)
        {
            using IServiceScope scope = useBindNine.CreateScope();
            using var dbx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbx.Database.Migrate();
            return useBindNine;
        }
    }
}