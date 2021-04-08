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

        public string ServTitle { get; set; }

        public int Rate { get; set; }

        public int MaxEmpNo{ get;set; }

        public int MaxClientNo { get; set; }


        public ICollection<Appointment> Appointments { get; set; }
    }
}
