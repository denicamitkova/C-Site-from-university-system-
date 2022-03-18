using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kursova.ActionFilters;
using Kursova.Database;
using Kursova.Entities;
using Kursova.ViewModels.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kursova.Controllers
{
    [AuthenticationFilter]
    public class StudentsController : Controller
    {

        public IActionResult Index(IndexVM model)
        {
            MyDbContext context = new MyDbContext();
            model.Items = context.Students.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // save students to database
            MyDbContext context = new MyDbContext();
            Student item = new Student();
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Enrollment = model.Enrollment;

            context.Students.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        public IActionResult Delete(int id)
        {
            MyDbContext context = new MyDbContext();
            Student item = new Student();
            item.Id = id;
            //context.Students.Remove(item);
            //or
            //context.Entry(item).State = EntityState.Deleted;
            //or
            EntityEntry entry = context.Entry(item);
            entry.State = EntityState.Deleted;
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            MyDbContext context = new MyDbContext();
            Student item = context.Students
                                    .Where(s => s.Id == id)
                                    .FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "Students");
            }
            EditVM model = new EditVM();
            model.Id = item.Id;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            model.Enrollment = item.Enrollment;
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // save students to database
            MyDbContext context = new MyDbContext();
            Student item = new Student();
            item.Id = model.Id;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Enrollment = model.Enrollment;

            context.Students.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }
    }
}
