using System.Threading;
using System.Threading.Tasks;

namespace BindNineCore.Abstraction
{
    /// <summary>
    /// Writes changes to bind9
    /// </summary>
    public interface IBindApplySettings
    {
        Task SaveAsync(CancellationToken token = default);
    }
}