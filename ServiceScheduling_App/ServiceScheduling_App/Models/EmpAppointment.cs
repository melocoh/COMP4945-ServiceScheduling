using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class EmpAppointment
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
