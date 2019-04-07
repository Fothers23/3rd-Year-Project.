using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Reviews.Where(x => x.Game.GameID == id).ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Reviews/Create
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Create(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.GameID == gameId);
            return View(new Review { Game = game });
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Create([Bind("Game,GraphicQuality,Playability,StoryCharacterDevelopment," +
            "GameplayControls,Multiplayer,OverallRating,Pros,Cons,WrittenReview,Summary,DatePosted")] Review review)
        {
            if (ModelState.IsValid)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    review.User = user;
                }
                var game = await _context.Games.FirstOrDefaultAsync(x => x.GameID == review.Game.GameID);
                review.Game = game;
                review.OverallRating = CalculateOverallRating(review);
                review.DatePosted = DateTime.Now;

                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = review.Game.GameID } );
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,GraphicQuality,Playability,StoryCharacterDevelopment," +
            "GameplayControls,Multiplayer,OverallRating,Pros,Cons,WrittenReview," +
            "Summary,DatePosted")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.OverallRating = CalculateOverallRating(review);

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
                return RedirectToAction(nameof(Details), new { id = review.ReviewID });
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.Include(r => r.Game).FirstOrDefaultAsync(x => x.ReviewID == id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = review.Game.GameID });
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }

        private double CalculateOverallRating(Review viewModel)
        {
            return (viewModel.GraphicQuality + viewModel.Playability
                    + viewModel.StoryCharacterDevelopment + viewModel.GameplayControls
                    + viewModel.Multiplayer) / 5.0;
        }

        [Authorize(Roles = "Requester")]
        public async Task<IActionResult> Rate(int? id)
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

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Requester")]
        public async Task<IActionResult> Rate(int id, [Bind("ReviewID,GameID,GraphicQuality,Playability," +
            "StoryCharacterDevelopment,GameplayControls,Multiplayer,OverallRating,Pros,Cons,WrittenReview," +
            "Summary,ReviewRating,DatePosted")] Review review)
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
                return RedirectToAction(nameof(Details), new { id = review.ReviewID});
            }
            return View(review);
        }
    }
}
