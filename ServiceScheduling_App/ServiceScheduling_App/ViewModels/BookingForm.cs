using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.ViewModels
{
    public class BookingForm
    {
        public string ServTitle { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Location { get; set; }

        public DayOfWeek Day { get; set; }
    }
}
