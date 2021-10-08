using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using cd_c_loginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace cd_c_loginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
        
        public HomeController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                //System.Console.WriteLine("TESTING1");
                if(_context.Users.Any(u => u.Email == user.Email))
                {
                    //System.Console.WriteLine("TESTING2");
                    ModelState.AddModelError("Email", "The email address you entered has already been registered with this site.");
                    
                    return View("Index", user);
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                _context.Add(user);
                _context.SaveChanges();
                return View("loginPage");
            }
            else
            {
                //System.Console.WriteLine("TESTING3");
                return View("Index", user);
            }
        }

        [HttpGet("loginPage")]
        public IActionResult LoginPage()
        {
            return View("LoginPage");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                //System.Console.WriteLine("TESTING1");
                var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.Email);

                if(userInDb == null)
                {
                    //System.Console.WriteLine("TESTING2");
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("loginPage", userSubmission);
                }
                var hasher = new PasswordHasher<LoginUser>();

                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

                if(result == 0)
                {
                    //System.Console.WriteLine("TESTING3");
                    ModelState.AddModelError("Password", "Invalid Email/Password.");
                    
                    return View("loginPage", userSubmission);
                }
                
                // var loggedInUser = new User
                // {
                //     Email = userSubmission.Email
                // };
                HttpContext.Session.SetInt32("loggedInUser", userInDb.UserId);
                //System.Console.WriteLine($"{userInDb.UserId}");
                return RedirectToAction("Success");
            }
            else
            {
                return View("loginPage", userSubmission);
            }
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            //System.Console.WriteLine("TESTING1");
            if(HttpContext.Session.GetInt32("loggedInUser") == null)
            {
                //System.Console.WriteLine("TESTING2");
                return View("loginpage");
            }
            return View("Success");
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("loginPage");
        }
        

    }
}