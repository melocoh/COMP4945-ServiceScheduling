using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cstestproject2.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public string Email { get; set; }
    }
}
