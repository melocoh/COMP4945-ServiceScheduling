﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class EmpShift
    {
        public int EmpShiftId { get; set; }
        public int EmpId { get; set; }
        public Employee Employee { get; set; }

        public int ServiceShiftId { get; set; }
        public ServiceShift ServiceShift { get; set; }
    }
}
