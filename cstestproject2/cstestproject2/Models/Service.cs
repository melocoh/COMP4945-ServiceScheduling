using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cstestproject2.Models
{
    public class Service
    {
        [Key]
        public int ServId { get; set; }

        [Display(Name = "Service Title")]
        public string ServTitle { get; set; }

        [Display(Name = "Rate ($/session)")]
        public int Rate { get; set; }

        [Display(Name = "Max Number of Employees")]
        public int MaxEmpNo{ get;set; }

        [Display(Name = "Max Number of Clients")]
        public int MaxClientNo { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
