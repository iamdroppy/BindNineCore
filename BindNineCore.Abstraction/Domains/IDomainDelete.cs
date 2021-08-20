using System;
using System.Threading;
using System.Threading.Tasks;

namespace BindNineCore.Abstraction.Domains
{
    /// <summary>
    /// Exposes methods for domain deletion
    /// </summary>
    public interface IDomainDelete
    {
        /// <summary>
        /// Deletes a domain given the domain's Id.
        /// </summary>
        /// <param name="domainId">Domain Id</param>
        /// <param name="token">Cancel token</param>
        /// <returns>No exceptions</returns>
        Task DeleteDomainAsync(Guid domainId, CancellationToken token = default);
    }
}