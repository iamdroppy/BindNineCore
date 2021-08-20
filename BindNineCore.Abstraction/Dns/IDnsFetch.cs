using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction.Domains;

namespace BindNineCore.Abstraction.Dns
{
    /// <summary>
    /// Exposes methods to fetch a list of DNS given a domain.
    /// </summary>
    public interface IDnsFetch
    {
        /// <summary>
        /// Gets all the dns given an exact <param name="domain">domain name</param>
        /// </summary>
        /// <param name="domain">Domain (exact)</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The list of DNS from <param name="domain"></param></returns>
        /// <exception cref="ArgumentNullException"><param name="domain"></param></exception>
        /// <exception cref="Exception"><param name="domain">Domain</param> doesn't exist or there's is not an exact match.</exception>
        Task<IEnumerable<IDns>> GetDnsByDomainAsync([NotNull] string domain, CancellationToken token = default);
        
        /// <summary>
        /// Gets all the dns given its <param name="domainId">registration id</param>
        /// </summary>
        /// <param name="domainId">Domain (exact)</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The list of DNS from <param name="domainId">its parent</param></returns>
        /// <exception cref="Exception">Domain not found.</exception>
        Task<IEnumerable<IDns>> GetDnsByDomainAsync(Guid domainId, CancellationToken token = default);
                
        /// <summary>
        /// Gets all the dns given its <param name="domain">domain</param>
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The list of DNS from <param name="domain"></param></returns>
        /// <exception cref="ArgumentNullException"><param name="domain"></param></exception>
        /// <exception cref="Exception">Domain not found.</exception>
        Task<IEnumerable<IDns>> GetDnsByDomainAsync([NotNull] IDomain domain, CancellationToken token = default);
    }
}