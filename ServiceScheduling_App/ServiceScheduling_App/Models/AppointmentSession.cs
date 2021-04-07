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
        [Key]
        public int AppSessionId { get; set; }

        [ForeignKey("JobType")]
        public int AppId { get; set;}

        public int SessionNo { get; set; }

        public List<string> Statuses { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
