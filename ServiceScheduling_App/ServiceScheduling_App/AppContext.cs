using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceScheduling_App.Models;

namespace ServiceScheduling_App
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<JobType> JobTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CertificationType> CertificationTypes { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<EmpCertification> EmpCertifications { get; set; }

        public DbSet<ServiceShift> ServiceShifts { get; set; }

        public DbSet<EmpShift> EmpShifts { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AppointmentSession> AppointmentSession { get; set; }

        public DbSet<EmpAppointment> EmpAppointments { get; set; }

        public DbSet<ClientAppointment> ClientAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table creation
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<CertificationType>().ToTable("CertificationType");
            modelBuilder.Entity<ServiceType>().ToTable("ServiceType");
            modelBuilder.Entity<ServiceShift>().ToTable("ServiceShift");

            // One to many with Appointment and Appointment Sessions
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<AppointmentSession>().ToTable("AppointmentSession");

            modelBuilder.Entity<AppointmentSession>()
            .HasOne<Appointment>(s => s.Appointment)
            .WithMany(g => g.AppointmentSessions)
            .HasForeignKey(s => s.AppId);

            // One to many with Appointment and ServiceShift
            modelBuilder.Entity<Appointment>()
            .HasOne<ServiceShift>(s => s.ServiceShift)
            .WithMany(g => g.Appointments)
            .HasForeignKey(s => s.ServiceShiftId);

            // Intermediate table creation with composite keys
            modelBuilder.Entity<EmpCertification>().HasKey(ec => new { ec.EmpId, ec.CertId });

            modelBuilder.Entity<EmpCertification>()
                .HasOne(pt => pt.Employee)
                .WithMany(p => p.EmpCertifications)
                .HasForeignKey(pt => pt.EmpId);

            modelBuilder.Entity<EmpCertification>()
                .HasOne(pt => pt.CertificationType)
                .WithMany(t => t.EmpCertifications)
                .HasForeignKey(pt => pt.CertId);

            modelBuilder.Entity<EmpShift>().HasKey(es => new { es.EmpShiftId, es.EmpId });

            modelBuilder.Entity<EmpShift>()
                .HasOne(pt => pt.Employee)
                .WithMany(p => p.EmpShifts)
                .HasForeignKey(pt => pt.EmpId);

            modelBuilder.Entity<EmpShift>()
                .HasOne(pt => pt.ServiceShift)
                .WithMany(p => p.EmpShifts)
                .HasForeignKey(pt => pt.ServiceShiftId);

            modelBuilder.Entity<EmpAppointment>().HasKey(ea => new { ea.EmpId, ea.AppId });

            modelBuilder.Entity<EmpAppointment>()
                .HasOne(pt => pt.Employee)
                .WithMany(p => p.EmpAppointments)
                .HasForeignKey(pt => pt.EmpId);

            modelBuilder.Entity<EmpAppointment>()
                .HasOne(pt => pt.Appointment)
                .WithMany(p => p.EmpAppointments)
                .HasForeignKey(pt => pt.AppId);

            modelBuilder.Entity<ClientAppointment>().HasKey(ca => new { ca.ClientId, ca.AppId });

            modelBuilder.Entity<ClientAppointment>()
                .HasOne(pt => pt.Client)
                .WithMany(p => p.ClientAppointments)
                .HasForeignKey(pt => pt.ClientId);

            modelBuilder.Entity<ClientAppointment>()
                .HasOne(pt => pt.Appointment)
                .WithMany(p => p.ClientAppointments)
                .HasForeignKey(pt => pt.AppId);
        }
    }
}
