using System;
using Kursova.Entities;
using Kursova.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kursova.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        public MyDbContext()
        {
            Users = this.Set<User>();
            Courses = this.Set<Course>();
            Enrollments = this.Set<Enrollment>();
            Students = this.Set<Student>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLexpress;Database=ProjectDb;User Id=Denica;Password=213021_deni");
        }
    }
}