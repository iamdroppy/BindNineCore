using System;

namespace BindNineCore.Exceptions
{
    public class DomainNotFoundException : BindNineCoreException
    {
        public Guid DomainId { get; }
        public string Domain { get; }

        public DomainNotFoundException(Guid domainId, string domain) 
            : base($"The domain {domain} ({domainId}) was not found.")
        {
            DomainId = domainId;
            Domain = domain;
        }

        public DomainNotFoundException(Guid domainId) : base($"The domain `{domainId}` was not found.")
        {
            DomainId = domainId;
        }
    }
}