﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceScheduling_App;

namespace ServiceScheduling_App.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceScheduling_App.Models.Appointment", b =>
                {
                    b.Property<int>("AppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServiceShiftId")
                        .HasColumnType("int");

                    b.Property<double>("TotalFee")
                        .HasColumnType("float");

                    b.HasKey("AppId");

                    b.HasIndex("ServiceShiftId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.AppointmentSession", b =>
                {
                    b.Property<int>("AppSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SessionNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("AppSessionId");

                    b.HasIndex("AppId");

                    b.ToTable("AppointmentSession");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.CertificationType", b =>
                {
                    b.Property<int>("CertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertTitle")
                        .HasColumnType("varchar(50)");

                    b.HasKey("CertId");

                    b.ToTable("CertificationType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("ClientId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ClientAppointment", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.HasKey("ClientId", "AppId");

                    b.HasIndex("AppId");

                    b.ToTable("ClientAppointments");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpAppointment", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.HasKey("EmpId", "AppId");

                    b.HasIndex("AppId");

                    b.ToTable("EmpAppointments");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpCertification", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("CertId")
                        .HasColumnType("int");

                    b.HasKey("EmpId", "CertId");

                    b.HasIndex("CertId");

                    b.ToTable("EmpCertifications");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpShift", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceShiftId")
                        .HasColumnType("int");

                    b.HasKey("EmpId", "ServiceShiftId");

                    b.HasIndex("ServiceShiftId");

                    b.ToTable("EmpShifts");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("EmpId");

                    b.HasIndex("JobId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.JobType", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("JobTitle")
                        .HasColumnType("varchar(50)");

                    b.HasKey("JobId");

                    b.ToTable("JobType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceShift", b =>
                {
                    b.Property<int>("ServiceShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("SerLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeStart")
                        .HasColumnType("time");

                    b.HasKey("ServiceShiftId");

                    b.HasIndex("ServId");

                    b.ToTable("ServiceShift");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceType", b =>
                {
                    b.Property<int>("ServId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CertificationRqt")
                        .HasColumnType("int");

                    b.Property<int>("MaxNoClient")
                        .HasColumnType("int");

                    b.Property<int>("MaxNoEmp")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("ServTitle")
                        .HasColumnType("varchar(50)");

                    b.HasKey("ServId");

                    b.HasIndex("CertificationRqt")
                        .IsUnique();

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Appointment", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.ServiceShift", "ServiceShift")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceShift");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.AppointmentSession", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.Appointment", "Appointment")
                        .WithMany("AppointmentSessions")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ClientAppointment", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.Appointment", "Appointment")
                        .WithMany("ClientAppointments")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceScheduling_App.Models.Client", "Client")
                        .WithMany("ClientAppointments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpAppointment", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.Appointment", "Appointment")
                        .WithMany("EmpAppointments")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceScheduling_App.Models.Employee", "Employee")
                        .WithMany("EmpAppointments")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpCertification", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.CertificationType", "CertificationType")
                        .WithMany("EmpCertifications")
                        .HasForeignKey("CertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceScheduling_App.Models.Employee", "Employee")
                        .WithMany("EmpCertifications")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertificationType");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.EmpShift", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.Employee", "Employee")
                        .WithMany("EmpShifts")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceScheduling_App.Models.ServiceShift", "ServiceShift")
                        .WithMany("EmpShifts")
                        .HasForeignKey("ServiceShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("ServiceShift");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Employee", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.JobType", "JobType")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceShift", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.ServiceType", "ServiceType")
                        .WithMany("ServiceShifts")
                        .HasForeignKey("ServId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceType", b =>
                {
                    b.HasOne("ServiceScheduling_App.Models.CertificationType", "CertificationType")
                        .WithOne("ServiceType")
                        .HasForeignKey("ServiceScheduling_App.Models.ServiceType", "CertificationRqt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertificationType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentSessions");

                    b.Navigation("ClientAppointments");

                    b.Navigation("EmpAppointments");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.CertificationType", b =>
                {
                    b.Navigation("EmpCertifications");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Client", b =>
                {
                    b.Navigation("ClientAppointments");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.Employee", b =>
                {
                    b.Navigation("EmpAppointments");

                    b.Navigation("EmpCertifications");

                    b.Navigation("EmpShifts");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.JobType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceShift", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("EmpShifts");
                });

            modelBuilder.Entity("ServiceScheduling_App.Models.ServiceType", b =>
                {
                    b.Navigation("ServiceShifts");
                });
#pragma warning restore 612, 618
        }
    }
}
