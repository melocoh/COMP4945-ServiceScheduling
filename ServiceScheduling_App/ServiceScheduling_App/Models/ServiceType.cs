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
        public string ServTitle { get; set; }

        [ForeignKey("CertificationType")]
        public int CertificationRqt { get; set; }

        public int MaxNoEmp { get; set; }

        public int MaxNoClient { get; set; }

        public double Rate { get; set; }

        // References ServiceType object
        //public virtual Appointment Appointment { get; set; }

        // References Employee object
        public virtual CertificationType CertificationType { get; set; }
    }
}
