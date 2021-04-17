using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.ViewModels
{
    public class BookingForm
    {
        //public List<SelectListItem> ServTitle { get; set; }

        //public List<SelectListItem> Location { get; set; }

        //public List<SelectListItem> Day { get; set; }

        public string ServTitle { get; set; }

        public string Location { get; set; }

        public string Day { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Min Date Range")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Max Date Range")]
        public DateTime EndDate { get; set; }

        //public BookingForm(List<SelectListItem> service, List<SelectListItem> location, List<SelectListItem> day)
        //{
        //    ServTitle = service;
        //    Location = location;
        //    Day = day;
        //}
    }
}
