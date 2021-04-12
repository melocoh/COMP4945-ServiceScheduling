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
        public string FullName { get; set; }

        [ForeignKey("JobType")] //Foreign Key attribute takes another Model class name
        public int JobId { get; set; }

        //[RegularExpression("^[\\w -\\.] +@([\\w -] +\\.)+[\\w-]{2,4}$")]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
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
