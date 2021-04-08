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

                // Look for any ailments
                //if (context.Clients != null && context.Clients.Any())
                //    return;   // DB has already been seeded


                //var jobTypes = DummyData.GetJobTypes().ToArray();
                //context.JobTypes.AddRange(jobTypes);
                //context.SaveChanges();

                //var employees = DummyData.GetEmployees().ToArray();
                //context.Employees.AddRange(employees);
                //context.SaveChanges();

                //var clients = DummyData.GetClients().ToArray();
                //context.Clients.AddRange(clients);
                //context.SaveChanges();

                //var certificationtypes = DummyData.GetCertificationTypes().ToArray();
                //context.CertificationTypes.AddRange(certificationtypes);
                //context.SaveChanges();

                //var servicetypes = DummyData.GetServiceTypes().ToArray();
                //context.ServiceTypes.AddRange(servicetypes);
                //context.SaveChanges();

                //var appointments = DummyData.GetAppointments().ToArray();
                //context.Appointments.AddRange(appointments);
                //context.SaveChanges();

                var appointmentSessions = DummyData.GetAppointmentSessions().ToArray();
                context.AppointmentSession.AddRange(appointmentSessions);
                context.SaveChanges();

                //var serviceShifts = DummyData.GetServiceShifts().ToArray();
                //context.ServiceShifts.AddRange(serviceShifts);
                //context.SaveChanges();

                //var empShifts = DummyData.GetEmpShifts().ToArray();
                //context.EmpShifts.AddRange(empShifts);
                //context.SaveChanges();

                //var empCertifications = DummyData.GetEmpCertifications().ToArray();
                //context.EmpCertifications.AddRange(empCertifications);
                //context.SaveChanges();

                //var empClientAppointments = DummyData.GetClientAppointments().ToArray();
                //context.ClientAppointments.AddRange(empClientAppointments);
                //context.SaveChanges();

                //var empAppointments = DummyData.GetEmpAppointments().ToArray();
                //context.EmpAppointments.AddRange(empAppointments);
                //context.SaveChanges();


            }
        }

        public static List<JobType> GetJobTypes()
        {
            List<JobType> jobTypes = new List<JobType>() {
            new JobType {JobTitle = "Sound Cloud Rapper"},
            new JobType {JobTitle = "Nurse"},
            new JobType {JobTitle = "Therapist"},
            new JobType {JobTitle = "Teacher"}
            };
            return jobTypes;
        }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>() {
            new Employee {
                FullName = "Nicki Minaj",
                JobId = 1,
                Email = "nicki@mail.com",
                Password = "1234"
                },
            new Employee {
                FullName = "Jason Chi Fai Lee",
                JobId = 2,
                Email = "cflee@mail.com",
                Password = "456"
                },
            new Employee {
                FullName = "Saem Lee",
                JobId = 3,
                Email = "slee@mail.com",
                Password = "hehe"
                },
            new Employee {
                FullName = "Eric NumbaOne",
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
            new Client {
                FullName = "Melody No",
                Email = "moh@mail.com",
                Password = "1234"
                },
            new Client {
                FullName = "Vincent Go",
                Email = "vin@mail.com",
                Password = "2345"
                },
            new Client {
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
            new CertificationType {CertTitle = "Hamburger Degree"},
            new CertificationType {CertTitle = "Nursing License"},
            new CertificationType {CertTitle = "Psychology Degree"},
            new CertificationType {CertTitle = "Astrology Diploma"}
            };
            return certificationTypes;
        }

        public static List<ServiceType> GetServiceTypes()
        {
            List<ServiceType> serviceTypes = new List<ServiceType>() {
            new ServiceType {
                ServTitle = "Teach",
                CertificationRqt = 4,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                },
            new ServiceType {
                ServTitle = "Clean",
                CertificationRqt = 2,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                },
           new ServiceType {
                ServTitle = "Rap",
                CertificationRqt = 1,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                }
            };
            return serviceTypes;
        }

        public static List<EmpCertification> GetEmpCertifications()
        {
            List<EmpCertification> empCertifications = new List<EmpCertification>() {
                new EmpCertification
                {
                    EmpId = 1,
                    CertId = 4
                },
                new EmpCertification
                {
                    EmpId = 2,
                    CertId = 2
                },
                new EmpCertification
                {
                    EmpId = 3,
                    CertId = 1
                },
                new EmpCertification
                {
                    EmpId = 4,
                    CertId = 3
                }
            };

            return empCertifications;
        }

        public static List<ServiceShift> GetServiceShifts()
        {
            List<ServiceShift> serviceShifts = new List<ServiceShift>() {
                new ServiceShift
                {
                    ServId = 2,
                    DayOfWeek = DayOfWeek.Monday,
                    TimeStart = new TimeSpan (7,30,00),
                    TimeEnd = new TimeSpan (15,30,00)
                },
                new ServiceShift
                {
                    ServId = 2,
                    DayOfWeek = DayOfWeek.Tuesday,
                    TimeStart = new TimeSpan (07,30,00),
                    TimeEnd = new TimeSpan (15,30,00)
                },
                new ServiceShift
                {
                    ServId = 2,
                    DayOfWeek = DayOfWeek.Friday,
                    TimeStart = new TimeSpan (07,30,00),
                    TimeEnd = new TimeSpan (15,30,00)
                },
                new ServiceShift
                {
                    ServId = 3,
                    DayOfWeek = DayOfWeek.Thursday,
                    TimeStart = new TimeSpan (17,00,00),
                    TimeEnd = TimeSpan.Zero
                },
                new ServiceShift
                {
                    ServId = 4,
                    DayOfWeek = DayOfWeek.Saturday,
                    TimeStart = new TimeSpan (12,30,00),
                    TimeEnd = TimeSpan.Zero
                }
            };

            return serviceShifts;
        }


        public static List<EmpShift> GetEmpShifts()
        {
            List<EmpShift> empShifts = new List<EmpShift>() {
                new EmpShift
                {
                    ServiceShiftId = 2,
                    EmpId = 1
                },
                new EmpShift
                {
                    ServiceShiftId = 3,
                    EmpId = 1
                },
                new EmpShift
                {
                    ServiceShiftId = 4,
                    EmpId = 1
                },
                new EmpShift
                {
                    ServiceShiftId = 5,
                    EmpId = 2
                },
                new EmpShift
                {
                    ServiceShiftId = 6,
                    EmpId = 3
                }
            };

            return empShifts;
        }


        public static List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>() {
                new Appointment
                {
                    ServId = 2,
                    EntryDate = DateTime.Now,
                    TotalFee = 42069
                },
                new Appointment
                {
                    ServId = 3,
                    EntryDate = DateTime.Now,
                    TotalFee = 69420
                }
            };
            return appointments;
        }

        public static List<AppointmentSession> GetAppointmentSessions()
        {
            List<AppointmentSession> appointmentsSessions = new List<AppointmentSession>() {
                new AppointmentSession
                {
                    AppId = 1,
                    SessionNo = 1,
                    StartDateTime = new DateTime (2021, 11, 16, 12, 00, 00),
                    EndDateTime = new DateTime (2021, 11, 16, 13, 00, 00),
                    Status = "Approved"
                },
                new AppointmentSession
                {
                    AppId = 1,
                    SessionNo = 2,
                    StartDateTime = new DateTime (2021, 11, 23, 12, 00, 00),
                    EndDateTime = new DateTime (2021, 11, 23, 13, 00, 00),
                    Status = "Pending"
                },
                new AppointmentSession
                {
                    AppId = 1,
                    SessionNo = 3,
                    StartDateTime = new DateTime (2021, 11, 30, 12, 00, 00),
                    EndDateTime = new DateTime (2021, 11, 30, 13, 00, 00),
                    Status = "Pending"
                },
                new AppointmentSession
                {
                    AppId = 2,
                    SessionNo = 1,
                    StartDateTime = new DateTime (2021, 12, 16, 09, 00, 00),
                    EndDateTime = new DateTime (2021, 12, 16, 09, 30, 00),
                    Status = "Denied"
                }
            };
            return appointmentsSessions;
        }









        //public static List<ClientAppointment> GetClientAppointments()
        //{
        //    List<ClientAppointment> clientAppointments = new List<ClientAppointment>() {
        //        new ClientAppointment
        //        {
        //            AppId = 0,
        //            ClientId = 0
        //        }
        //    };

        //    return clientAppointments;
        //}



        //public static List<EmpAppointment> GetEmpAppointments()
        //{
        //    List<EmpAppointment> empAppointments = new List<EmpAppointment>() {
        //        new EmpAppointment
        //        {
        //            AppId = 0,
        //            EmpId = 0
        //        }
        //    };

        //    return empAppointments;
        //}






    }
}
