using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Dns;
using BindNineCore.Abstraction.Domains;
using BindNineCore.Core.Database;
using BindNineCore.Core.Database.Entities;
using BindNineCore.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BindNineCore.Core.Services.Dns
{
    internal class DnsCreate : BaseService<DnsCreate>, IDnsCreate
    {
        public DnsCreate(ILogger<DnsCreate> logger, ApplicationDbContext dbContext) : base(logger, dbContext) {}

        
        public async Task<IDns> AddDnsToDomainAsync(Guid domainId, [Subdomain] string subdomain, RecordType recordType, [NotNull] string value, CancellationToken token = default)
        {
            ThrowIfInvalidRecord(subdomain, recordType, value);
            
            var domain = await _dbContext.Domains.Include(s => s.Dns).SingleAsync(s => s.Id == domainId, cancellationToken: token);
            if (domain == null) throw new DomainNotFoundException(domainId);
            
            if (domain.Dns.Any(s => s.Subdomain == subdomain)) throw new SubdomainAlreadyExistsException(subdomain, domain.Domain);

            DnsEntity entity = new()
            {
                Subdomain = subdomain,
                RecordType = recordType,
                Value = value
            };
            
            domain.Dns.Add(entity);
            
            await _dbContext.SaveChangesAsync(token);
            
            return entity;
        }

        /// <summary>
        /// An alternative to the Guid domainId overload.
        /// If <exception cref="DomainNotFoundException"></exception> is thrown, it will try to rethrow with more information.
        /// </summary>
        public Task<IDns> AddDnsToDomainAsync(IDomain domain, [Subdomain] string subdomain, RecordType recordType, [NotNull] string value, CancellationToken token = default)
        {
            if (Equals(domain, null)) throw new ArgumentNullException(nameof(domain));
            if (Equals(subdomain, null)) throw new ArgumentNullException(nameof(subdomain));
            if (Equals(value, null)) throw new ArgumentNullException(nameof(value));
            
            try
            {
                return AddDnsToDomainAsync(domain.Id, subdomain, recordType, value, token);
            }
            catch (DomainNotFoundException)
            {
                if (domain.Domain == null) throw; // rethrow if domain is invalid.
                throw new DomainNotFoundException(domain.Id, domain.Domain);
            }
        }
        
        private void ThrowIfInvalidRecord(string subdomain, RecordType recordType, string value)
        {
            if (!NetworkValidation.Subdomain(subdomain)) throw new InvalidSubdomainException(subdomain);

            if (Equals(value, null)) throw new ArgumentNullException(nameof(value));

            if (recordType == RecordType.Alias && !NetworkValidation.IpAddress(value))
                throw new InvalidAliasRecordValueException(value);
        }
    }
}