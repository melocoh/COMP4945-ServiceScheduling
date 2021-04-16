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

        [ForeignKey("ServiceShift")] // Foreign key that references ServiceType
        public int ServiceShiftId { get; set; }

        public DateTime EntryDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")] // input validation
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Must fit currency format")] // Input validation
        public double TotalFee { get; set; }

        /*****************************************/

        // References ServiceType object 
        // @relation one-to-one
        //public virtual ServiceType ServiceType { get; set; }


        // References ServiceShift object 
        // @relation one-to-many
        public virtual ServiceShift ServiceShift { get; set; }


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
