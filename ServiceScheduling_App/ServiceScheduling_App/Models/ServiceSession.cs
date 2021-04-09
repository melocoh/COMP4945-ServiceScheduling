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

        public string DayOfTheWeek { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Time { get; set; }
    }
}
