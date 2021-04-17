using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters and spaces only please")] // Input validation
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")] // Input validation
        [Required] // Input validation
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required] // Input validation
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }

        public ICollection<ClientAppointment> ClientAppointments { get; set; }
    }
}
