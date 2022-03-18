using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursova.ViewModels.Courses
{
    public class CreateVM
    {
        [Required(ErrorMessage = "*This field is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*This field is required!")]
        public int Credits { get; set; }
    }
}