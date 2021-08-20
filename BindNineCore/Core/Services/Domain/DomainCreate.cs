using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Config;
using BindNineCore.Core.Database;
using BindNineCore.Core.Database.Entities;
using BindNineCore.Exceptions;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Domain
{
    internal class DomainCreate : BaseService<DomainCreate>, IDomainCreate
    {
        private readonly BindConfig _cfg;
        private readonly IDomainFetch _domainFetch;

        public DomainCreate(ILogger<DomainCreate> logger, ApplicationDbContext dbContext, BindConfig cfg, IDomainFetch domainFetch) : base(logger, dbContext)
        {
            _cfg = cfg;
            _domainFetch = domainFetch;
        }

        public async Task<IDomain> CreateDomainAsync(string domain, string nameserver = null, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            if (!NetworkValidation.Hostname(domain)) throw new InvalidDomainException(domain);

            if (await _domainFetch.HasDomainAsync(domain, token)) throw new DomainAlreadyExistsException(domain);

            DomainEntity entity = new ()
            {
                Domain = domain,
                Dns = new List<DnsEntity>()
                {
                    new () { RecordType = RecordType.Nameserver, Subdomain = "ns", Value = nameserver ?? _cfg.DefaultNameserver, CanBeDeleted = false },
                }
            };

            await _dbContext.Domains.AddAsync(entity, token);
            await _dbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}