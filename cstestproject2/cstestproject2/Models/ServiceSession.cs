using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cstestproject2.Models
{
    public class ServiceSession
    {
        [Key]
        public int ServSessionId { get; set; }

        public string Service { get; set; }

        public string Location { get; set; }

        [Display(Name = "Day of the Week")]
        public string DayOfTheWeek { get; set; }

        // uses time range based on the shifts
        public string Time { get; set; }

        // Real time object
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        //public DateTime Time { get; set; }
    }
}
