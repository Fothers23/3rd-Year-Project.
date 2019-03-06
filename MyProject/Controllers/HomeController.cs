using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ApplicationDbContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ApplicationUser user = userManager.FindByIdAsync(userId).Result;
                return View(user);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "My third year project.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ApplicationUser user = userManager.FindByIdAsync(userId).Result;
                return View(user);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DeveloperName,CompanyDescription")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(user);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(user);
        }

        private bool UserExists(string id)
        {
            return db.Users.Any(u => u.Id == id);
        }
    }
}
