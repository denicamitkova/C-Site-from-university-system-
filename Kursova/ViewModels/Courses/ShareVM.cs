using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursova.Entities;

namespace Kursova.ViewModels.Courses
{
    public class ShareVM
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Course Courses { get; set; }
        public List<Student> SharedWith { get; set; }
        public List<Student> Students { get; set; }
    }
}
