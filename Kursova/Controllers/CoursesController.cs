using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kursova.ActionFilters;
using Kursova.Database;
using Kursova.Entities;
using Kursova.ViewModels.Courses;
using Microsoft.EntityFrameworkCore;

namespace Kursova.Controllers
{
    [AuthenticationFilter]
    public class CoursesController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            MyDbContext context = new MyDbContext();
            model.Items = context.Courses.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Share(int id)
        {
            ShareVM model = new ShareVM();

            MyDbContext context = new MyDbContext();

            //Course item = context.Courses
            //                    .Where(c => c.CourseId == id)
            //                    .FirstOrDefault();
            model.Courses = context.Courses
                                .Where(c => c.CourseId == id)
                                .FirstOrDefault();

            model.SharedWith = context.Enrollments
                                .Include(utc => utc.Student)
                                .Include(utc => utc.Course)
                                .Where(i => i.CourseId == model.Courses.CourseId)
                                .Select(id => id.Student)
                                .ToList();

            model.Students = context.Students.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Share(ShareVM model)
        {
            MyDbContext context = new MyDbContext();

            Enrollment enrollment = new Enrollment();
            enrollment.StudentId = model.StudentId;
            enrollment.CourseId = model.CourseId;

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            return RedirectToAction("Share", "Courses", new { id = model.CourseId });
        }

        public IActionResult RevokeAccess(int StudentId, int CourseId)
        {
            MyDbContext context = new MyDbContext();

            Enrollment item = context.Enrollments
                            .Where(utc => utc.StudentId == StudentId &&
                                          utc.CourseId == CourseId)
                            .FirstOrDefault();

            if (item != null)
            {
                context.Enrollments.Remove(item);
                context.SaveChanges();
            }
            return RedirectToAction("Share", "Courses", new { id = CourseId });
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
            // save to database
            MyDbContext context = new MyDbContext();
            Course item = new Course();

            item.Title = model.Title;
            item.Credits = model.Credits;

            context.Courses.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }


        public IActionResult Delete(int id)
        {
            MyDbContext context = new MyDbContext();
            Course item = new Course();
            item.CourseId = id;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            MyDbContext context = new MyDbContext();

            Course item = context.Courses
                                .Where(c => c.CourseId == id)
                                .FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Index", "Courses");
            }

            EditVM model = new EditVM();
            model.CourseId = item.CourseId;
            model.Title = item.Title;
            model.Credits = item.Credits;


            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // save to database
            MyDbContext context = new MyDbContext();

            Course item = new Course();

            item.CourseId = model.CourseId;
            item.Title = model.Title;
            item.Credits = model.Credits;

            context.Courses.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }
    }
}