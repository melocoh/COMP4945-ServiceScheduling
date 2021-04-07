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
        public int AppDetailsId { get; set; }

        [ForeignKey("ServiceType")]
        public int ServId { get; set; }

        public List<string> Statuses { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public DateTime EntryDate { get; set; }

        public float TotalFee { get; set; }
    }
}
