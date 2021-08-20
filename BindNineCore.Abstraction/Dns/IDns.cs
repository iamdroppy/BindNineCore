using System;
using BindNineCore.Abstraction.Domains;

namespace BindNineCore.Abstraction.Dns
{
    /// <summary>
    /// DNS entry, which is many to one <see cref="IDomain"/>
    /// </summary>
    public interface IDns
    {
        Guid Id { get; }
        string Subdomain { get; }
        RecordType RecordType { get; }
        string Value { get; }
        bool CanBeDeleted { get; }
    }
}