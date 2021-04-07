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

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }



        // References ServiceType object
        public virtual ServiceType ServiceType { get; set; }

        // References EmpShift object
        public ICollection<EmpShift> EmpShifts { get; set; }
    }
}
