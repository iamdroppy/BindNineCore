using System;

namespace BindNineCore.Exceptions
{
    public class DnsNotFoundException : BindNineCoreException
    {
        public Guid? Id { get; }

        public DnsNotFoundException(Guid id) : base("Could not find DNS record by id " + id)
        {
            Id = id;
        }
    }
}