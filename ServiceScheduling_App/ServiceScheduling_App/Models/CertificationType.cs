using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class CertificationType
    {
        [Key] // Primary key
        public int CertId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters and spaces only please")] // Input validation
        public string CertTitle { get; set; }

        /*****************************************/

        // References ServiceType object
        // @relation one-to-one
        public virtual ServiceType ServiceType { get; set; }

        // References EmpCertifications collection
        // @relation many-to-many
        public ICollection<EmpCertification> EmpCertifications { get; set; }

    }
}
