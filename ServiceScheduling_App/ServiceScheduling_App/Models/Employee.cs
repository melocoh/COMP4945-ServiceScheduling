using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters and spaces only please")] // Input validation
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [ForeignKey("JobType")]
        public int JobId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")] // Input validation
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }

        // References JobType object
        public virtual JobType JobType { get; set; }

        // References EmpCertification object
        public ICollection<EmpCertification> EmpCertifications { get; set; }

        // References EmpAppointment object
        public ICollection<EmpAppointment> EmpAppointments { get; set; }

        // References EmpShift object
        public ICollection<EmpShift> EmpShifts { get; set; }
    }
}
