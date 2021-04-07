using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceScheduling_App.Models;

namespace ServiceScheduling_App
{
    public class AppContext: DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) {}

        public DbSet<JobType> JobTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CertificationType> CertificationTypes { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        //public DbSet<EmpCertification> EmpCertifications { get; set; }

        public DbSet<ServiceShift> ServiceShifts { get; set; }

        //public DbSet<EmpShift> EmpShifts { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        //public DbSet<EmpAppointment> EmpAppointments { get; set; }

        //public DbSet<ClientAppointment> ClientAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<CertificationType>().ToTable("CertificationType");
            modelBuilder.Entity<ServiceType>().ToTable("ServiceType");
            modelBuilder.Entity<EmpCertification>().ToTable("EmpCertification");
            modelBuilder.Entity<ServiceShift>().ToTable("ServiceShift");
            modelBuilder.Entity<EmpShift>().ToTable("EmpShift");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<EmpAppointment>().ToTable("EmpAppointment");
            modelBuilder.Entity<ClientAppointment>().ToTable("ClientAppointment");
        }
    }
}
