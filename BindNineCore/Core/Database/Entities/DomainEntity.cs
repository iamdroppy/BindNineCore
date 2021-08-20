using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Domains;
using DotLiquid;

namespace BindNineCore.Core.Database.Entities
{
    public class DomainEntity : IDomain, ILiquidizable
    {
        [Key]
        public Guid Id { get; set; }

        private string _domain;
        [NotNull, Hostname, MaxLength(64)]
        public string Domain
        { 
            get => _domain;
            set => _domain = value?.ToLowerInvariant();
        }

        public int Retry { get; set; } = 86400;
        public int Expire { get; set; } = 3600;
        public int NegativeCacheTtl { get; set; } = 604800;

        public virtual ICollection<DnsEntity> Dns { get; set; }
        
        public object ToLiquid()
        {
            return this;
        }
    }
}