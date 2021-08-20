using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction.Dns;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Dns
{
    internal class DnsFetch : BaseService<DnsFetch>, IDnsFetch
    {
        public DnsFetch(ILogger<DnsFetch> logger, ApplicationDbContext dbContext) : base(logger, dbContext) {}
        
        public async Task<IEnumerable<IDns>> GetDnsByDomainAsync(string domain, CancellationToken token = default)
        {
            domain = domain?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(domain));
            var domainEntity = await _dbContext.Domains.AsNoTracking().Include(s=>s.Dns).SingleAsync(s => s.Domain == domain, cancellationToken: token);
            return domainEntity.Dns;
        }

        public async Task<IEnumerable<IDns>> GetDnsByDomainAsync(Guid domainId, CancellationToken token = default)
        {
            var domainEntity = await _dbContext.Domains.AsNoTracking().Include(s=>s.Dns).SingleAsync(s=>s.Id == domainId, cancellationToken: token);
            return domainEntity.Dns;
        }

        public async Task<IEnumerable<IDns>> GetDnsByDomainAsync(IDomain domain, CancellationToken token = default)
        {
            if (Equals(domain, null)) throw new ArgumentNullException(nameof(domain));
            return await GetDnsByDomainAsync(domain.Id, token);
        }
    }
}