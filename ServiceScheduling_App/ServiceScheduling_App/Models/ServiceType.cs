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

        public string ServTitle { get; set; }

        [ForeignKey("CertificationType")]
        public int CertificationRqt { get; set; }

        public int MaxNoEmp { get; set; }

        public int MaxNoClient { get; set; }

        public float Rate { get; set; }

    }
}
