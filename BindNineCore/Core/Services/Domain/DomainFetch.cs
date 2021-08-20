using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Domain
{
    internal class DomainFetch : BaseService<DomainFetch>, IDomainFetch
    {
        public DomainFetch(ILogger<DomainFetch> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async Task<IDomain> FindDomainAsync(string domain, CancellationToken token = default)
        {
            if (Equals(domain, null)) throw new ArgumentNullException(nameof(domain));
            token.ThrowIfCancellationRequested();

            return await _dbContext.Domains.AsNoTracking().SingleAsync(s => s.Domain == domain, cancellationToken: token);
        }

        public Task<bool> HasDomainAsync(string domain, CancellationToken token = default)
        {
            return _dbContext.Domains.AsNoTracking().AnyAsync(s => s.Domain == domain, cancellationToken: token);
        }

        public IEnumerable<IDomain> GetDomains(Expression<Func<IDomain, bool>> where = null)
        {
            var query = _dbContext.Domains.AsNoTracking();
            return where != null 
                    ? query.Where(where)
                    : query;
        }
    }
}