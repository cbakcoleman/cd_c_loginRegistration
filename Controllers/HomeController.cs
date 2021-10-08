using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using cd_c_loginRegistration.Models;
using Microsoft.AspNetCore.Identity;

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
                if(_context.Users.Any(user => user.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "The email address you entered has already been registered with this site.");
                    
                    return RedirectToAction("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", new { userId = user.UserId});
            }
            else
            {
                return View("Index", user);
            }
        }
    }
}