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

        [Display(Name = "Day of Week")]
        [Required] // Input validation
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Start Time")]
        [Required] // Input validation
        public TimeSpan TimeStart { get; set; }

        [Display(Name = "End Time")]
        [Required] // Input validation
        public TimeSpan TimeEnd { get; set; }

        [Display(Name = "Location")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")] // Input validation
        public string SerLocation { get; set; }

        // References ServiceType object
        public virtual ServiceType ServiceType { get; set; }

        // References EmpShift object
        public ICollection<EmpShift> EmpShifts { get; set; }

        // References Appointment object
        // @relation one-to-many
        public ICollection<Appointment> Appointments { get; set; }
    }
}
