﻿using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext _db) => db = _db;

        [HttpGet]
        public IActionResult Index()
        {
            return View("IndexWithForm");
        }

        [HttpPost]
        public IActionResult Index(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("GameList");
            }
            return View("IndexWithForm");
        }

        [HttpGet]
        public IActionResult GameList()
        {
            GameList gameList = new GameList();

            gameList.Games = db.Games.ToList<Game>();
            gameList.NumberOfGames = gameList.Games.Count;

            return View(gameList);
        }

        [HttpGet]
        public IActionResult Review()
        {
            return View("ReviewForm");
        }

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
