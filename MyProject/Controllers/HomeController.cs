using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db;

        //[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(JobSubmissionModel submittedJob)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.JobSubmissions.Add(submittedJob);
        //        db.SaveChanges();

        //        return View(submittedJob);
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult JobList()
        //{
        //    AvailableJobViewModel availableJobViewModel = new AvailableJobViewModel();

        //    availableJobViewModel.Jobs = db.JobSubmissions.ToList<JobSubmissionModel>();
        //    availableJobViewModel.NumberOfJobs = availableJobViewModel.Jobs.Count;

        //    return View(availableJobViewModel);
        //}

        [Authorize]
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
    }
}
