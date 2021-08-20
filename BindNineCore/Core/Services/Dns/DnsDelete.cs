using System;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Dns;
using BindNineCore.Core.Database;
using BindNineCore.Core.Database.Entities;
using BindNineCore.Core.Services.Domain;
using BindNineCore.Exceptions;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Dns
{
    internal class DnsDelete : BaseService<DnsDelete>, IDnsDelete
    {
        public DnsDelete(ILogger<DnsDelete> logger, ApplicationDbContext dbContext) : base(logger, dbContext) {}

        public async Task DeleteRecordAsync(Guid id, CancellationToken token = default)
        {
            DnsEntity dns = await _dbContext.Dns.FindAsync(id);
            if (dns == null)
                throw new DnsNotFoundException(id);
            
            _dbContext.Remove(dns);
            await _dbContext.SaveChangesAsync(token);
        }

        public Task DeleteRecordAsync(IDns id, CancellationToken token = default)
        {
            if (Equals(id, null)) throw new ArgumentNullException(nameof(id));
            return DeleteRecordAsync(id.Id, token);
        }
    }
}