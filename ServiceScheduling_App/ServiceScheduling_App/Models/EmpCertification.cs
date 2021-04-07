using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class EmpCertification
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CertificationId { get; set; }
        public CertificationType CertificationType { get; set; }
    }
}
