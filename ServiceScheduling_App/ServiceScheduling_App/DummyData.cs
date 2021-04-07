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

                var certificationTypes = DummyData.GetCertificationTypes().ToArray();
                context.CertificationTypes.AddRange(certificationTypes);
                context.SaveChanges();

                var serviceTypes = DummyData.GetServiceTypes().ToArray();
                context.ServiceTypes.AddRange(serviceTypes);
                context.SaveChanges();

                var appointments = DummyData.GetAppointments().ToArray();
                context.Appointments.AddRange(appointments);
                context.SaveChanges();

                var appointmentSessions = DummyData.GetAppointmentSessions().ToArray();
                context.AppointmentSession.AddRange(appointmentSessions);
                context.SaveChanges();

                var serviceShifts = DummyData.GetServiceShifts().ToArray();
                context.ServiceShifts.AddRange(serviceShifts);
                context.SaveChanges();

                var empShifts = DummyData.GetEmpShifts().ToArray();
                context.EmpShifts.AddRange(empShifts);
                context.SaveChanges();

                var empCertifications = DummyData.GetEmpCertifications().ToArray();
                context.EmpCertifications.AddRange(empCertifications);
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
                JobId = 0,
                Email = "nicki@mail.com",
                Password = "1234"
                },
            new Employee {
                FullName = "Jason Chi Fai Lee",
                JobId = 0,
                Email = "cflee@mail.com",
                Password = "456"
                },
            new Employee {
                FullName = "Saem Lee",
                JobId = 0,
                Email = "slee@mail.com",
                Password = "hehe"
                },
            new Employee {
                FullName = "Eric NumbaOne",
                JobId = 0,
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
                Password = "234"
                },
            new Client {
                FullName = "Risham JojosAdventure",
                Email = "risham@mail.com",
                Password = "123"
                }
            };
            return clients;
        }

        public static List<CertificationType> GetCertificationTypes()
        {
            List<CertificationType> certificationTypes = new List<CertificationType>() {
            new CertificationType {CertTitle = "Sound Cloud Rapper"},
            new CertificationType {CertTitle = "Nurse"},
            new CertificationType {CertTitle = "Therapist"},
            new CertificationType {CertTitle = "Teacher"}
            };
            return certificationTypes;
        }

        public static List<ServiceType> GetServiceTypes()
        {
            List<ServiceType> serviceTypes = new List<ServiceType>() {
            new ServiceType {
                ServTitle = "Melody No",
                CertificationRqt = 1,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                },
            new ServiceType {
                ServTitle = "Melody No",
                CertificationRqt = 1,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                },
           new ServiceType {
                ServTitle = "Melody No",
                CertificationRqt = 1,
                MaxNoEmp = 1,
                MaxNoClient = 2,
                Rate = 30
                }
            };
            return serviceTypes;
        }




        public static List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>() {
                new Appointment
                {
                    AppId = 0,
                    ServId = 0,
                    EntryDate =DateTime.Now,
                    TotalFee = 42069f
                }
            };
            return appointments;
        }


        public static List<AppointmentSession> GetAppointmentSessions()
        {
            List<AppointmentSession> appointmentsSessions = new List<AppointmentSession>() {
                new AppointmentSession
                {
                    AppSessionId = 0,
                    AppId = 0,
                    SessionNo = 0,
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Status = "Completed"
                }
            };
            return appointmentsSessions;
        }



        public static List<ServiceShift> GetServiceShifts()
        {
            List<ServiceShift> serviceShifts = new List<ServiceShift>() {
                new ServiceShift
                {
                    ServiceShiftId = 0,
                    ServId = 0,
                    DayOfWeek = DayOfWeek.Monday,
                    TimeStart = TimeSpan.Zero,
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
                    ServiceShiftId = 0,
                    EmpId = 0
                }
            };

            return empShifts;
        }


        public static List<EmpCertification> GetEmpCertifications()
        {
            List<EmpCertification> empCertifications = new List<EmpCertification>() {
                new EmpCertification
                {
                    EmpId = 0,
                    CertId = 0
                }
            };

            return empCertifications;
        }



        public static List<ClientAppointment> GetClientAppointments()
        {
            List<ClientAppointment> clientAppointments = new List<ClientAppointment>() {
                new ClientAppointment
                {
                    AppId = 0,
                    ClientId = 0
                }
            };

            return clientAppointments;
        }



        public static List<EmpAppointment> GetEmpAppointments()
        {
            List<EmpAppointment> empAppointments = new List<EmpAppointment>() {
                new EmpAppointment
                {
                    AppId = 0,
                    EmpId = 0
                }
            };

            return empAppointments;
        }






    }
}
