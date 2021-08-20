using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BindNineCore.Abstraction.Domains
{
    /// <summary>
    /// Exposes methods to create a domain
    /// </summary>
    public interface IDomainCreate
    {
        /// <summary>
        /// Creates a domain
        ///
        /// By default, it will use the nameservers provided by the config. Custom name servers can be added.
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <param name="nameserver">NS1</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Created domain</returns>
        /// <exception cref="Exception">If domain already exists</exception>
        /// <exception cref="ArgumentNullException">If <param name="domain"></param> is null or not a valid hostname</exception>
        Task<IDomain> CreateDomainAsync([NotNull, Hostname] string domain, string nameserver = null, CancellationToken token = default);
    }
}