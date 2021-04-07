using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class EmpCertification
    {
        public int EmpId { get; set; }
        public Employee Employee { get; set; }

        public int CertId { get; set; }
        public CertificationType CertificationType { get; set; }
    }
}
