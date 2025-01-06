using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PassengerAppDbContext : DbContext
    {
        public PassengerAppDbContext(DbContextOptions<PassengerAppDbContext> options) : base(options)
        {

        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bus>().HasQueryFilter(bus => !bus.IsDeleted);
            modelBuilder.Entity<Driver>().HasQueryFilter(driver => !driver.IsDeleted);
            modelBuilder.Entity<Flight>().HasQueryFilter(flight => !flight.IsDeleted);

            modelBuilder.Entity<Bus>()
                         .HasOne(bus => bus.Driver)
                         .WithOne(driver => driver.Bus)
                         .HasForeignKey<Bus>(bus => bus.Id);

            modelBuilder.Entity<Driver>()
                        .HasOne(driver => driver.Bus)
                        .WithOne(bus => bus.Driver)
                        .HasForeignKey<Driver>(driver => driver.Id);
        }
    }
}
