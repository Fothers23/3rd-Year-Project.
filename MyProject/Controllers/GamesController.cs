using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

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
            GameDetailsViewModel viewModel = await GetViewModelFromGame(game);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("GameID,GraphicQuality,Playability," +
            "StoryCharacterDevelopment,GameplayControls,Multiplayer,Pros,Cons,WrittenReview," +
            "Summary")] GameDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review();
                review.GraphicQuality = viewModel.GraphicQuality;
                review.Playability = viewModel.Playability;
                review.StoryCharacterDevelopment = viewModel.StoryCharacterDevelopment;
                review.GameplayControls = viewModel.GameplayControls;
                review.Multiplayer = viewModel.Multiplayer;
                review.OverallRating = (viewModel.GraphicQuality + viewModel.Playability
                    + viewModel.StoryCharacterDevelopment + viewModel.GameplayControls
                    + viewModel.Multiplayer) / 5.0;
                review.Pros = viewModel.Pros;
                review.Cons = viewModel.Cons;
                review.WrittenReview = viewModel.WrittenReview;
                review.Summary = viewModel.Summary;

                Game game = await _context.Games
                    .SingleOrDefaultAsync(m => m.GameID == viewModel.GameID);

                if (game == null)
                {
                    return NotFound();
                }

                review.MyGame = game;
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                viewModel = await GetViewModelFromGame(game);
            }
            return View(viewModel);
        }

        private async Task<GameDetailsViewModel> GetViewModelFromGame(Game game)
        {
            GameDetailsViewModel viewModel = new GameDetailsViewModel();

            viewModel.Game = game;

            List<Review> reviews = await _context.Reviews
                .Where(x => x.MyGame == game).ToListAsync();

            viewModel.Reviews = reviews;
            return viewModel;
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        public async Task<IActionResult> EditReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(int id, [Bind("ReviewID,GraphicQuality,Playability," +
            "StoryCharacterDevelopment,GameplayControls,Multiplayer,Pros,Cons,WrittenReview," +
            "Summary")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewID))
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
            return View(review);
        }

        public async Task<IActionResult> DeleteReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
