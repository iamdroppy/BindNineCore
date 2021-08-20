using System;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Core.Database;
using BindNineCore.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Domain
{
    internal class DomainDelete : BaseService<DomainDelete>, IDomainDelete
    {
        public DomainDelete(ILogger<DomainDelete> logger, ApplicationDbContext dbContext) : base(logger, dbContext) {}
        
        public async Task DeleteDomainAsync(Guid domainId, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var domainToDelete = await _dbContext.Domains.Include(s=>s.Dns).SingleOrDefaultAsync(s=>s.Id == domainId, cancellationToken: token);
            if (domainToDelete == null) throw new DomainNotFoundException(domainId);

            foreach (var dns in domainToDelete.Dns)
                _dbContext.Dns.Remove(dns);
            
            _dbContext.Domains.Remove(domainToDelete);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}