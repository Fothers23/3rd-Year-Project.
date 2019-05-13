using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GamesController(ApplicationDbContext context, IHostingEnvironment appEnvironment,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /* Retrieves and creates a list of all games from the 
         * database and returns the corresponding Index View. 
        */
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Retrieves action input by user.
            ViewData["DeveloperSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RewardSortParm"] = string.IsNullOrEmpty(sortOrder) ? "reward_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var games = from g in _context.Games.Include(x => x.Developer)
                          select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Title.Contains(searchString)
                                       || g.Developer.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(g => g.Developer.Name);
                    break;
                case "Reward":
                    games = games.OrderBy(g => g.ReviewReward);
                    break;
                case "reward_desc":
                    games = games.OrderByDescending(g => g.ReviewReward);
                    break;
                default:
                    games = games.OrderBy(g => g.Developer.Name);
                    break;
            }
            return View(await games.AsNoTracking().ToListAsync());
        }

        /* Retrieves the Details view and populates it with 
         * Game details of the Game with the corresponding id.
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game game = await _context.Games.Include(u => u.Developer)
                .SingleOrDefaultAsync(m => m.GameID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
        
        // Retrieves the Create View.
        [Authorize(Roles = "Requester")]
        public IActionResult Create()
        {
            return View();
        }

        // Deals with the input information from the Create View.
        [Authorize(Roles = "Requester")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Picture,Title,Developer,Description,GameLink,AgeRating," +
            "Genre,NumberOfPlayers,AvailablePlatforms,ReviewQuantity,ReviewReward,DatePosted")] IFormFile file, Game game)
        {
            if (file == null || file.Length == 0) return Content("Image file not selected");
            string pathRoot = _appEnvironment.WebRootPath;
            string pathToImages = pathRoot + "\\images\\" + file.FileName;
            string image = file.FileName;

            // Adds image to folder.
            using (var stream = new FileStream(pathToImages, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ViewData["Image"] = image;

            if (ModelState.IsValid)
            {
                // Assigns user as Developer of the Game.
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    game.Developer = user;
                }
                // Sets the DatePosted as the current date and time.
                game.DatePosted = DateTime.Now;
                game.Picture = image; // Sets Picture as the image uploaded.
                _context.Add(game); // Adds Game to the database.
                await _context.SaveChangesAsync(); // Saves changes to the database.
                return RedirectToAction(nameof(Index)); // Redirects the user to Game Index upon submission.
            }
            return View(game); // Returns the Create view if info input incorrectly.
        }

        /* Retrieves the Edit view and populates it with 
         * Game details of the Game with the corresponding id.
         */
        [Authorize(Roles = "Requester")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // Deals with the input information from the Edit View.
        [Authorize(Roles = "Requester")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,Picture,Title,Developer,Description,GameLink," +
            "AgeRating,Genre,NumberOfPlayers,AvailablePlatforms,ReviewQuantity,ReviewReward,Budget,DatePosted")] Game game)
        {
            if (id != game.GameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game); // Updates the database entry.
                    await _context.SaveChangesAsync(); // Saves changes to the database.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirects the user to Game Index upon submission.
            }
            return View(game); // Returns the Edit view if info input incorrectly.
        }

        /* Retrieves the Delete view and populates it with 
         * Game details of the Game with the corresponding id.
         */
        [Authorize(Roles = "Requester")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.GameID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // Deals with the input information from the Delete view.
        [Authorize(Roles = "Requester")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id); // Finds the Game in the database.
            _context.Games.Remove(game); // Deletes the Game from the database.
            await _context.SaveChangesAsync(); // Save changes to the database.
            return RedirectToAction(nameof(Index)); // Redirects the user to the Game Index upon submission.
        }

        // Checks the id matches the id of a game in the database.
        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameID == id);
        }
    }
}