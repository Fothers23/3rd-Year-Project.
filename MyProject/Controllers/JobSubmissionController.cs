using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class JobSubmissionController : Controller
    {
        private ApplicationDbContext db;

        public JobSubmissionController(ApplicationDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(JobSubmissionModel submittedJob)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(submittedJob);
                db.SaveChangesAsync();

                return RedirectToAction("JobList");
            }
            return View();
        }

        public IActionResult JobList()
        {
            JobListViewModel availableJobs = new JobListViewModel();

            availableJobs.Jobs = db.Jobs.ToList<JobSubmissionModel>();
            availableJobs.NumberOfJobs = availableJobs.Jobs.Count;

            return View(availableJobs);
        }
    }
}