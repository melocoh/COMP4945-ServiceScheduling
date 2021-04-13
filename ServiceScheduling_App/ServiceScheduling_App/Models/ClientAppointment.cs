using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class ClientAppointment
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int AppId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
