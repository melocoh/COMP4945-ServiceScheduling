﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class CertificationType
    {
        [Key]
        public int CertId { get; set; }
        public string CertTitle { get; set; }


        // References ServiceType object
        public virtual ServiceType ServiceType { get; set; }

        // References EmpCertifications object
        public ICollection<EmpCertification> EmpCertifications { get; set; }

    }
}
