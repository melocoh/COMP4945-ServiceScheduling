using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceScheduling_App.Models;

namespace ServiceScheduling_App.Migrations
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                if (context.Clients != null && context.Clients.Any())
                    return;   // DB has already been seeded

                var jobTypes = DummyData.GetJobTypes().ToArray();
                context.JobTypes.AddRange(jobTypes);
                context.SaveChanges();

                var employees = DummyData.GetEmployees().ToArray();
                context.Employees.AddRange(employees);
                context.SaveChanges();

                var clients = DummyData.GetClients().ToArray();
                context.Clients.AddRange(clients);
                context.SaveChanges();

                var certificationtypes = DummyData.GetCertificationTypes().ToArray();
                context.CertificationTypes.AddRange(certificationtypes);
                context.SaveChanges();

                var servicetypes = DummyData.GetServiceTypes().ToArray();
                context.ServiceTypes.AddRange(servicetypes);
                context.SaveChanges();

                var empCertifications = DummyData.GetEmpCertifications().ToArray();
                context.EmpCertifications.AddRange(empCertifications);
                context.SaveChanges();

                var serviceShifts = DummyData.GetServiceShifts().ToArray();
                context.ServiceShifts.AddRange(serviceShifts);
                context.SaveChanges();

                var empShifts = DummyData.GetEmpShifts().ToArray();
                context.EmpShifts.AddRange(empShifts);
                context.SaveChanges();

                var appointments = DummyData.GetAppointments().ToArray();
                context.Appointments.AddRange(appointments);
                context.SaveChanges();

                var appointmentSessions = DummyData.GetAppointmentSessions().ToArray();
                context.AppointmentSession.AddRange(appointmentSessions);
                context.SaveChanges();

                var empClientAppointments = DummyData.GetClientAppointments().ToArray();
                context.ClientAppointments.AddRange(empClientAppointments);
                context.SaveChanges();

                var empAppointments = DummyData.GetEmpAppointments().ToArray();
                context.EmpAppointments.AddRange(empAppointments);
                context.SaveChanges();
            }
        }

        public static List<JobType> GetJobTypes()
        {
            List<JobType> jobTypes = new List<JobType>() {
            new JobType {JobTitle = "Sound Cloud Rapper"}, // 1
            new JobType {JobTitle = "Teacher"},            // 2
            new JobType {JobTitle = "Nurse"},              // 3
            new JobType {JobTitle = "Janitor"}             // 4
            };
            return jobTypes;
        }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>() {
            new Employee {  // 1
                FullName = "Nicki Minaj",
                JobId = 1,
                Email = "nicki@mail.com",
                Password = "1234"
                },
            new Employee {  // 2
                FullName = "Jason Chi Fai Lee",
                JobId = 2,
                Email = "cflee@mail.com",
                Password = "456"
                },
            new Employee {  // 3
                FullName = "Saem Lee",
                JobId = 3,
                Email = "slee@mail.com",
                Password = "hehe"
                },
            new Employee {  // 4
                FullName = "Eric NumbaOne",
                JobId = 4,
                Email = "eric@mail.com",
                Password = "1234"
                },
            new Employee {  // 5
                FullName = "Melody NumbaOne",
                JobId = 4,
                Email = "eric@mail.com",
                Password = "1234"
                }
            };
            return employees;
        }

        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>() {
            new Client {  // 1
                FullName = "Melody No",
                Email = "moh@mail.com",
                Password = "1234"
                },
            new Client {  // 2
                FullName = "Vincent Go",
                Email = "vin@mail.com",
                Password = "2345"
                },
            new Client {  //3
                FullName = "Risham JojosAdventure",
                Email = "risham@mail.com",
                Password = "4321"
                }
            };
            return clients;
        }

        public static List<CertificationType> GetCertificationTypes()
        {
            List<CertificationType> certificationTypes = new List<CertificationType>() {
            new CertificationType {CertTitle = "Sound Cloud Verified"},  // 1 
            new CertificationType {CertTitle = "Teaching License"},      // 2
            new CertificationType {CertTitle = "Nursing Degree"},        // 3
            new CertificationType {CertTitle = "Astrology Diploma"}      // 4
            };
            return certificationTypes;
        }

        public static List<ServiceType> GetServiceTypes()
        {
            List<ServiceType> serviceTypes = new List<ServiceType>() {
            new ServiceType {  // 1
                ServTitle = "Rap",
                CertificationRqt = 1,
                MaxNoEmp = 5,
                MaxNoClient = 20,
                Rate = 10
                },
            new ServiceType {  // 2
                ServTitle = "Teach",
                CertificationRqt = 2,
                MaxNoEmp = 1,
                MaxNoClient = 30,
                Rate = 30
                },
            new ServiceType {  // 3  
                ServTitle = "Give vaccination",
                CertificationRqt = 3,
                MaxNoEmp = 3,
                MaxNoClient = 1,
                Rate = 60
                },
            new ServiceType {  // 4   relationship to think about
                ServTitle = "Clean",
                CertificationRqt = 4,
                MaxNoEmp = 3,
                MaxNoClient = 1,
                Rate = 60
                }
            };

            return serviceTypes;
        }

        public static List<EmpCertification> GetEmpCertifications()
        {
            List<EmpCertification> empCertifications = new List<EmpCertification>() {
                new EmpCertification  // 1 
                {
                    EmpId = 1,
                    CertId = 1
                },
                new EmpCertification  // 1 
                {
                    EmpId = 3,
                    CertId = 1
                },
                new EmpCertification  // 1 
                {
                    EmpId = 4,
                    CertId = 1
                },
                new EmpCertification  // 2
                {
                    EmpId = 2,
                    CertId = 2
                },
                new EmpCertification  // 3
                {
                    EmpId = 3,
                    CertId = 3
                },
                new EmpCertification  // 4
                {
                    EmpId = 4,
                    CertId = 4
                }
            };

            return empCertifications;
        }

        public static List<ServiceShift> GetServiceShifts()
        {
            List<ServiceShift> serviceShifts = new List<ServiceShift>() {
                new ServiceShift  // 1
                {
                    ServId = 1,
                    DayOfWeek = DayOfWeek.Monday,
                    TimeStart = new TimeSpan (7,30,00),
                    TimeEnd = new TimeSpan (15,30,00),
                    SerLocation = "Burnaby"
                },
                new ServiceShift  // 2 
                {
                    ServId = 1,
                    DayOfWeek = DayOfWeek.Wednesday,
                    TimeStart = new TimeSpan (07,30,00),
                    TimeEnd = new TimeSpan (15,30,00),
                    SerLocation = "Burnaby"
                },
                new ServiceShift  // 3
                {
                    ServId = 2,
                    DayOfWeek = DayOfWeek.Friday,
                    TimeStart = new TimeSpan (07,30,00),
                    TimeEnd = new TimeSpan (15,30,00),
                    SerLocation = "Richmond"
                },
                new ServiceShift  // 4
                {
                    ServId = 3,
                    DayOfWeek = DayOfWeek.Thursday,
                    TimeStart = new TimeSpan (17,00,00),
                    TimeEnd = new TimeSpan (20,30,00),
                    SerLocation = "Burnaby"
                },
                new ServiceShift  // 5
                {
                    ServId = 4,
                    DayOfWeek = DayOfWeek.Saturday,
                    TimeStart = new TimeSpan (12,30,00),
                    TimeEnd = new TimeSpan (17,00,00),
                    SerLocation = "Richmond"
                },
                new ServiceShift  // 6
                {
                    ServId = 4,
                    DayOfWeek = DayOfWeek.Tuesday,
                    TimeStart = new TimeSpan (12,30,00),
                    TimeEnd = new TimeSpan (17,00,00),
                    SerLocation = "Vancouver"
                },
                new ServiceShift  // 7
                {
                    ServId = 4,
                    DayOfWeek = DayOfWeek.Tuesday,
                    TimeStart = new TimeSpan (12,30,00),
                    TimeEnd = new TimeSpan (17,00,00),
                    SerLocation = "Vancouver"
                },
                new ServiceShift  // 8
                {
                    ServId = 4,
                    DayOfWeek = DayOfWeek.Tuesday,
                    TimeStart = new TimeSpan (12,30,00),
                    TimeEnd = new TimeSpan (17,00,00),
                    SerLocation = "Vancouver"
                },
            };

            return serviceShifts;
        }


        public static List<EmpShift> GetEmpShifts()
        {
            List<EmpShift> empShifts = new List<EmpShift>() {
                new EmpShift  // 1
                {
                    ServiceShiftId = 1,
                    EmpId = 1
                },
                new EmpShift  // 1
                {
                    ServiceShiftId = 1,
                    EmpId = 3
                },
                new EmpShift  // 1
                {
                    ServiceShiftId = 1,
                    EmpId = 4
                },
                new EmpShift  // 2
                {
                    ServiceShiftId = 2,
                    EmpId = 1
                },
                new EmpShift  // 3
                {
                    ServiceShiftId = 7,
                    EmpId = 2
                },
                new EmpShift  // 4
                {
                    ServiceShiftId = 4,
                    EmpId = 3
                },
                new EmpShift  // 5
                {
                    ServiceShiftId = 5,
                    EmpId = 4
                },
                 new EmpShift  // 6
                {
                    ServiceShiftId = 6,
                    EmpId = 4
                }
            };

            return empShifts;
        }


        public static List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>() {
                new Appointment  // 1
                {
                    ServiceShiftId = 1,
                    EntryDate = DateTime.Now,
                    TotalFee = 42069
                },
                new Appointment  // 2
                {
                    ServiceShiftId = 2,
                    EntryDate = DateTime.Now,
                    TotalFee = 69420
                },
                new Appointment  // 2
                {
                    ServiceShiftId = 2,
                    EntryDate = new DateTime (2021, 11, 16, 12, 00, 00),
                    TotalFee = 100
                }
            };
            return appointments;
        }

        public static List<AppointmentSession> GetAppointmentSessions()
        {
            List<AppointmentSession> appointmentsSessions = new List<AppointmentSession>() {
                new AppointmentSession  // 1
                {
                    AppId = 1,
                    SessionNo = 1,
                    StartDateTime = new DateTime (2021, 11, 16, 12, 00, 00),
                    EndDateTime = new DateTime (2021, 11, 16, 13, 00, 00),
                    Status = "Completed"
                },
                new AppointmentSession  // 2
                {
                    AppId = 1,
                    SessionNo = 2,
                    StartDateTime = new DateTime (2021, 11, 23, 12, 00, 00),
                    EndDateTime = new DateTime (2021, 11, 23, 13, 00, 00),
                    Status = "Pending"
                },
                new AppointmentSession  // 4
                {
                    AppId = 2,
                    SessionNo = 1,
                    StartDateTime = new DateTime (2021, 10, 16, 09, 00, 00),
                    EndDateTime = new DateTime (2021, 10, 16, 09, 30, 00),
                    Status = "Denied"
                }
            };
            return appointmentsSessions;
        }

        public static List<ClientAppointment> GetClientAppointments()
        {
            List<ClientAppointment> clientAppointments = new List<ClientAppointment>() {
                new ClientAppointment  // 1
                {
                    AppId = 1,
                    ClientId = 1
                },
                new ClientAppointment  // 2
                {
                    AppId = 1,
                    ClientId = 2
                },
                new ClientAppointment  // 3
                {
                    AppId = 2,
                    ClientId = 2
                }
            };

            return clientAppointments;
        }


        public static List<EmpAppointment> GetEmpAppointments()
        {
            List<EmpAppointment> empAppointments = new List<EmpAppointment>() {
                new EmpAppointment  // 1
                {
                    AppId = 1,
                    EmpId = 1
                },
                new EmpAppointment  // 2
                {
                    AppId = 2,
                    EmpId = 2
                }
            };

            return empAppointments;
        }
    }
}
