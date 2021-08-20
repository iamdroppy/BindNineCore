using System;

namespace BindNineCore.Abstraction.Domains
{
    public interface IDomain
    {
        Guid Id { get; }
        
        [Hostname]
        string Domain { get; }
    }
}