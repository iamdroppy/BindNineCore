using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction.Domains;

namespace BindNineCore.Abstraction.Dns
{
    /// <summary>
    /// Exposes methods to create a DNS entry to a domain
    /// </summary>
    public interface IDnsCreate
    {
        /// <summary>
        /// Adds the dns to a domain (via <param name="domainId">domain's primary key</param>)
        /// </summary>
        /// <param name="domainId">Domain's registration id</param>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="recordType">Type of record</param>
        /// <param name="value">Value for the record</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>The created DNS</returns>
        /// <exception cref="ArgumentNullException"><param name="subdomain"></param></exception>
        /// <exception cref="ArgumentNullException"><param name="value"></param></exception>
        /// <exception cref="Exception">Subdomain is invalid or already exists for this domain</exception>
        Task<IDns> AddDnsToDomainAsync(Guid domainId, [Subdomain] string subdomain, RecordType recordType, [NotNull] string value,
            CancellationToken token = default);
        
        
        /// <summary>
        /// Adds the dns to a domain (via <param name="domain">domain's object</param>)
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="recordType">Type of record</param>
        /// <param name="value">Value for the record</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>The created DNS</returns>
        /// <exception cref="ArgumentNullException"><param name="subdomain"></param></exception>
        /// <exception cref="ArgumentNullException"><param name="value"></param></exception>
        /// <exception cref="Exception">Subdomain is invalid or already exists for this domain</exception>
        Task<IDns> AddDnsToDomainAsync([NotNull] IDomain domain, [Subdomain] string subdomain, RecordType recordType, [NotNull] string value,
            CancellationToken token = default);
    }
}