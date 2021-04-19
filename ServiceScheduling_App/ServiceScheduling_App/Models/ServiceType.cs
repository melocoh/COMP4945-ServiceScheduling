using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class ServiceType
    {
        [Key]
        public int ServId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters and spaces only please")] // Input validation
        public string ServTitle { get; set; }

        [ForeignKey("CertificationType")]
        public int CertificationRqt { get; set; }

        [Range(1, 20)] // input validation
        public int MaxNoEmp { get; set; }

        [Range(1, 20)] // input validation
        public int MaxNoClient { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")] // input validation    
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Must fit currency format")] // Input validation
        public double Rate { get; set; }

        // References EmpShift object
        public ICollection<ServiceShift> ServiceShifts { get; set; }

        // References Employee object
        public virtual CertificationType CertificationType { get; set; }
    }
}
