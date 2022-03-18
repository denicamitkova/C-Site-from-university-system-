using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kursova.ActionFilters;
using Kursova.Database;
using Kursova.ViewModels.Users;

namespace Kursova.Controllers
{
    public class UsersController : Controller
    {
        [AuthenticationFilter]
        public IActionResult Index(IndexVM model)
        {
            MyDbContext context = new MyDbContext();
            model.Items = context.Users.ToList();
            model.StudentItems = context.Students.ToList();
            model.CourseItems = context.Courses.ToList();
            model.EnrollmentItems = context.Enrollments.ToList();
            return View(model);
        }
    }
}
