using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursova.Entities;

namespace Kursova.ViewModels.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }
        public List<Student> StudentItems { get; set; }
        public List<Course> CourseItems { get; set; }
        //public List<Student> SharedWith { get; set; }
        public List<Enrollment> EnrollmentItems { get; set; }
    }
}