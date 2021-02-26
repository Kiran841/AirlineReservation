using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Assignment1B.Models;

namespace Assignment1B.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Booking>()
                .HasOne(s => s.Flight)
                .WithMany(c => c.Bookings)
                .HasForeignKey(s => s.FlightId)
                .HasConstraintName("FK_Bookings_FlightId");

            builder.Entity<OrderDetail>()
               .HasOne(s => s.Booking)
               .WithMany(c => c.OrderDetails)
               .HasForeignKey(s => s.BookingId)
               .HasConstraintName("FK_OrderDetails_BookingId");

            builder.Entity<Trip>()
              .HasOne(s => s.Booking)
              .WithMany(c => c.Trips)
              .HasForeignKey(s => s.BookingId)
              .HasConstraintName("FK_Trips_BookingId");

            builder.Entity<OrderDetail>()
              .HasOne(s => s.Order)
              .WithMany(c => c.OrderDetails)
              .HasForeignKey(s => s.OrderId)
              .HasConstraintName("FK_OrderDetails_OrderId");
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
