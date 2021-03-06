// <auto-generated />
using System;
using BindNineCore.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BindNineCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("BindNineCore.Core.Database.Entities.DnsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("CanBeDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("DomainId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RecordType")
                        .HasColumnType("int");

                    b.Property<string>("Subdomain")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Dns");
                });

            modelBuilder.Entity("BindNineCore.Core.Database.Entities.DomainEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Domain")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("BindNineCore.Core.Database.Entities.DnsEntity", b =>
                {
                    b.HasOne("BindNineCore.Core.Database.Entities.DomainEntity", "Domain")
                        .WithMany("Dns")
                        .HasForeignKey("DomainId");

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("BindNineCore.Core.Database.Entities.DomainEntity", b =>
                {
                    b.Navigation("Dns");
                });
#pragma warning restore 612, 618
        }
    }
}
