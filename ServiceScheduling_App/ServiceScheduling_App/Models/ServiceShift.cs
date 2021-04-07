using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class ServiceShift
    {
        [Key]
        public int ServiceShiftId { get; set; }

        [ForeignKey("ServiceType")]
        public int ServId { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }
    }
}
