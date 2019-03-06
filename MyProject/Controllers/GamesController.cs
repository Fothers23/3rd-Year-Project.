using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game game = await _context.Games
                .SingleOrDefaultAsync(m => m.GameID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        //private async Task<GameDetailsViewModel> GetViewModelFromGame(Game game)
        //{
        //    GameDetailsViewModel viewModel = new GameDetailsViewModel();

        //    viewModel.Game = game;

        //    List<Review> reviews = await _context.Reviews
        //        .Where(x => x.Game == game).ToListAsync();

        //    viewModel.Reviews = reviews;
        //    return viewModel;
        //}

        [Authorize(Roles = "Requester")]
        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [Authorize(Roles = "Requester")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Developer,Description,GameLink,AgeRating" +
            ",Genre,NumberOfPlayers,AvailablePlatforms,ReviewQuantity,ReviewReward,DatePosted")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
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

        // POST: Games/Edit/5
        [Authorize(Roles = "Requester")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,Title,Developer,Description,GameLink," +
            "AgeRating,Genre,NumberOfPlayers,AvailablePlatforms,ReviewQuantity,ReviewReward")] Game game)
        {
            if (id != game.GameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
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

        // POST: Games/Delete/5
        [Authorize(Roles = "Requester")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameID == id);
        }
    }
}