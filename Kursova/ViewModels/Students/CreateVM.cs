using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursova.ViewModels.Students
{
    public class CreateVM
    {
        [Required(ErrorMessage = "*This field is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*This field is required!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*This field is required!")]
        public DateTime Enrollment { get; set; }
    }
}
