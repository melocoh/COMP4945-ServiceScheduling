using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cstestproject2.Models
{

    public enum MonthEnum
    {
        January, 
    }

    public class Appointment
    {
        [Key]
        public int AppId { get; set; }

        //[ForeignKey ("Service")]
        //public int ServId { get; set; }

        [Display(Name = "Service Title")]
        public string ServTitle { get; set; }

        //public DayOfWeek Day { get; set; }


        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDateTime { get; set; }


        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "End Date Time")]
        public DateTime EndDateTime { get; set; }



        public virtual Service Service { get; set; }
    }
}
