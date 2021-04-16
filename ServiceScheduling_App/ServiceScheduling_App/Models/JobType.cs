using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class JobType
    {
        [Key]
        public int JobId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")] // Input validation
        public string JobTitle { get; set; }


        // References ServiceType object
        public virtual Employee Employee { get; set; }
    }
}
