using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BindNineCore.Abstraction;
using BindNineCore.Abstraction.Dns;
using DotLiquid;

namespace BindNineCore.Core.Database.Entities
{
    public class DnsEntity : IDns, ILiquidizable
    {
        [Key]
        public Guid Id { get; set; }
        
        public virtual DomainEntity Domain { get; set; } 
        
        private string _subdomain;
        [MaxLength(255)]
        public string Subdomain 
        { 
            get => _subdomain;
            set => _subdomain = value?.ToLowerInvariant();
        }

        public RecordType RecordType { get; set; }
        
        [MaxLength(255)]
        public string Value { get; set; }

        public bool CanBeDeleted { get; set; } = true;

        [NotMapped] public string ZoneRecordType => RecordType.GetShortName();

        public object ToLiquid()
        {
            return this;
        }
    }
}