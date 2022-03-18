using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kursova.ViewModels.Register;

namespace Kursova.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
            return RedirectToAction("Login", "Login");
        }
    }
}
