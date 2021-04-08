using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cstestproject2.Models;

namespace cstestproject2
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<ServiceSession> ServiceSessions { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table creation
            modelBuilder.Entity<ServiceSession>().ToTable("ServiceSession");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Employee>().ToTable("Employee");

            // One to many with Appointment and Service
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Service>().ToTable("Service");

            modelBuilder.Entity<Appointment>()
            .HasOne<Service>(s => s.Service)
            .WithMany(g => g.Appointments)
            .HasForeignKey(s => s.ServId);
        }
    }
}
