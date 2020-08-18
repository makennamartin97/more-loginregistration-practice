using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using loginregistration.Models;
using Microsoft.AspNetCore.Http;

namespace loginregistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User newuser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.email == newuser.email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
            
                    // You may consider returning to the View at this point
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newuser.pw = Hasher.HashPassword(newuser, newuser.pw);
                _context.Add(newuser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("userID", newuser.userID);
                return RedirectToAction("Dashboard");

                
                
            }
            else{
                return View("Index");

            }
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? userID = HttpContext.Session.GetInt32("userID");
            if (userID == null)
            {
                return RedirectToAction("Index");
            }
            User user = _context.Users.FirstOrDefault(u => u.userID == userID);
            return View(user);

        }
        [HttpPost("login")]
        public IActionResult Login(Login log)
        {
            if(ModelState.IsValid)
            {
            // If inital ModelState is valid, query for a user with provided email
                var userInDb = _context.Users.FirstOrDefault(u => u.email == log.loginemail);
            // If no user exists with provided email
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                var hash = new PasswordHasher<Login>();
                var result = hash.VerifyHashedPassword(log, userInDb.pw, log.loginpw);
                     //Result will either be 0 or 1.
                if(result == 0)
                {
                    ModelState.AddModelError("loginpw", "Invalid Email/Password");
                    return View("Index");
                         //Handle rejection and send them back to the form.
                }
                HttpContext.Session.SetInt32("userID", userInDb.userID);
                return RedirectToAction("Dashboard");
            }

            return View("Index");

        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }
}
