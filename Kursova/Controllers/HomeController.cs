using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Kursova.ActionFilters;
using Kursova.Database;
using Kursova.Entities;
using Kursova.ViewModels.Home;
using Kursova.ViewModels.Register;

namespace Kursova.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1.Create a dbContext
            MyDbContext context = new MyDbContext();

            // 2.Extract a user by username and password(from the model)
            User item = context.Users
                                .Where(u => u.Username == model.Username &&
                                            u.Password == model.Password)
                                        .FirstOrDefault();
            // 2.1. If user is found (null value is returned) then add validation error "Wrong Username or Password" and return the Login View
            if (item == null)
            {
                ModelState.AddModelError("AuthenticationFailed", "Wrong username or Password");
                return View(model);
            }

            //2.2. If User is found then add their details to the Session
            this.HttpContext.Session.SetString("loggedUser", model.Username);
            this.HttpContext.Session.SetString("LoggedUserUsername", model.Username);
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // save/register user in database

            MyDbContext context = new MyDbContext();
            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            context.Users.Add(item);
            context.SaveChanges();


            return RedirectToAction("Index", "Register");
        }

        public IActionResult Logout()
        {
            // 1. Remove the key "loggedUser" from the active session
            this.HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index", "Home");
        }

    }
}