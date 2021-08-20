using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BindNineCore.Abstraction.Dns
{
    /// <summary>
    /// Exposes methods to delete a DNS entry
    /// </summary>
    public interface IDnsDelete
    {
        /// <summary>
        /// Deletes a DNS record
        /// </summary>
        /// <param name="id">DNS unique id</param>
        /// <param name="token">CancellationToken</param>
        Task DeleteRecordAsync(Guid id, CancellationToken token = default);
        
        /// <summary>
        /// Deletes a DNS record
        /// </summary>
        /// <param name="dns">DNS object</param>
        /// <param name="token">CancellationToken</param>
        /// <exception cref="ArgumentNullException"><param name="dns"></param></exception>
        Task DeleteRecordAsync([NotNull] IDns dns, CancellationToken token = default);
    }
}