using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cstestproject2.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        [ForeignKey("JobType")]//Foreign Key attribute takes another Model class name
        public int JobID { get; set; }
        
        [RegularExpression("^[\\w -\\.] +@([\\w -] +\\.)+[\\w-]{2,4}$")]
        public string email { get; set; }
        public string password { get; set; }
    }
}
