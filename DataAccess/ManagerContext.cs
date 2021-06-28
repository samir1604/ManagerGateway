using ManagerGateway.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        {

        }
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {

        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>(entity => {
                entity.HasKey(p => p.Usn);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Address).IsRequired();
            });

            modelBuilder.Entity<Device>(entity => {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Gateway)
                .WithMany(p => p.Devices);

                entity.OwnsOne(p => p.Status)
                 .Property(p => p.Online);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
