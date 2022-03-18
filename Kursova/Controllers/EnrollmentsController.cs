using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kursova.Database;
using Kursova.Entities;

namespace Kursova.Controllers
{
    public class EnrollmentsController : Controller
    {

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            MyDbContext context = new MyDbContext();
            var myDbContext = context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await myDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            MyDbContext context = new MyDbContext();
            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,StudentId,CourseId")] Enrollment enrollment)
        {
            MyDbContext context = new MyDbContext();
            if (ModelState.IsValid)
            {
                context.Add(enrollment);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            MyDbContext context = new MyDbContext();
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,StudentId,CourseId")] Enrollment enrollment)
        {
            MyDbContext context = new MyDbContext();
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(enrollment);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            MyDbContext context = new MyDbContext();
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MyDbContext context = new MyDbContext();
            var enrollment = await context.Enrollments.FindAsync(id);
            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            MyDbContext context = new MyDbContext();
            return context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
