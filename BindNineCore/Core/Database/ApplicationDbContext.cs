using BindNineCore.Config;
using BindNineCore.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BindNineCore.Core.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DomainEntity> Domains { get; set; }
        public DbSet<DnsEntity> Dns { get; set; }
    }
}