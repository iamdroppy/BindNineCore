using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BindNineCore.Abstraction.Domains
{
    /// <summary>
    /// Exposes methods to find a certain domain.
    /// </summary>
    public interface IDomainFetch
    {
        /// <summary>
        /// Finds a domain by the exact domain name.
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Domain</returns>
        /// <exception cref="Exception">Throws an exception if the domain is not found.</exception>
        /// <exception cref="ArgumentNullException">If <param name="domain"></param> is null</exception>
        Task<IDomain> FindDomainAsync([Hostname] string domain, CancellationToken token = default);
        
        /// <summary>
        /// Finds a domain by the exact domain name.
        /// </summary>
        /// <param name="domainId">Domain unique id</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Domain</returns>
        /// <exception cref="Exception">Throws an exception if the domain is not found.</exception>
        Task<IDomain> FindDomainAsync([Hostname] Guid domainId, CancellationToken token = default);

        /// <summary>
        /// Checks if the domain exists on the database.
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Whether the domain exists or not.</returns>
        Task<bool> HasDomainAsync([Hostname] string domain, CancellationToken token = default);

        /// <summary>
        /// Gets a list of domains with a given predicate.
        /// </summary>
        /// <param name="where">Where predicate</param>
        /// <returns>The list of domains that matches <param name="where"></param></returns>
        IEnumerable<IDomain> GetDomains(Expression<Func<IDomain, bool>> where = null);
    }
}