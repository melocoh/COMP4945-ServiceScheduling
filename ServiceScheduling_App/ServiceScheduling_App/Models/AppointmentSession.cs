using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class AppointmentSession
    {
        [Key] // Primary key
        public int AppSessionId { get; set; }

        [ForeignKey("Appointment")] // Foreign key that references Appointment
        public int AppId { get; set;}

        public int SessionNo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        /*****************************************/

        // References Appointment object
        // @relation one-to-many
        public virtual Appointment Appointment { get; set; }
    }
}
