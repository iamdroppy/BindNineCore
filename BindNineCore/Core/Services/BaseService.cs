using System;
using System.Threading.Tasks;
using BindNineCore.Core.Database;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services
{
    internal abstract class BaseService<TService> : IDisposable, IAsyncDisposable where TService: BaseService<TService>
    {
        protected readonly ILogger<TService> _logger;
        protected readonly ApplicationDbContext _dbContext;

        protected BaseService(ILogger<TService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public virtual void Dispose()
        {
            _dbContext.Dispose();
        }

        public virtual ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}