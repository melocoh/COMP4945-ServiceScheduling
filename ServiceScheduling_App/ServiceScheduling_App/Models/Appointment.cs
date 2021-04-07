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
        [Key]
        public int AppId { get; set; }

        [ForeignKey("ServiceType")]
        public int ServId { get; set; }

        public DateTime EntryDate { get; set; }

        public float TotalFee { get; set; }

        public ICollection<EmpAppointment> EmpAppointments { get; set; }

        public ICollection<ClientAppointment> ClientAppointment { get; set; }
    }
}
