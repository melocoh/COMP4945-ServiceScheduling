using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class EmpShift
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ServiceShiftId { get; set; }
        public ServiceShift ServiceShift { get; set; }
    }
}
