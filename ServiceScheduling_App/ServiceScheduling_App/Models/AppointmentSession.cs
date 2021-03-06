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
        [Display(Name = "Appointment ID")]
        public int AppId { get; set;}

        [Required] // Input validation
        [Range(1, 10)] // range validation
        [Display(Name = "Session No.")]
        public int SessionNo { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")] // Input validation
        public string Status { get; set; }

        [Required] // Input validation
        [Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [Required] // Input validation
        [Display(Name = "End Date Time")]
        public DateTime EndDateTime { get; set; }

        /*****************************************/

        // References Appointment object
        // @relation one-to-many
        public virtual Appointment Appointment { get; set; }
    }

}
