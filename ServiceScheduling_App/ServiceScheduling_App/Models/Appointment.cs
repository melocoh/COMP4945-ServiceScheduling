using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class Appointment
    {
        [Key] // Primary key
        public int AppId { get; set; }

        [ForeignKey("ServiceType")] // Foreign key that references ServiceType
        public int ServId { get; set; }

        public DateTime EntryDate { get; set; }

        public double TotalFee { get; set; }

        /*****************************************/

        // References ServiceType object 
        // @relation one-to-one
        public virtual ServiceType ServiceType { get; set; }

        // References Appointment object
        // @relation one to many

        public ICollection<AppointmentSession> AppointmentSessions { get; set; }

        // References EmpAppointment collection
        // @relation many-to-many
        public ICollection<EmpAppointment> EmpAppointments { get; set; }

        // References ClientAppointment collection
        // @relation many-to-many
        public ICollection<ClientAppointment> ClientAppointments { get; set; }
    }
}
