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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        //[Authorize(Roles = "Crowdworker, Requester")]
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Reviews.Where(x => x.Game.GameID == id).ToListAsync());
        }

        // GET: Reviews/Details/5
        //[Authorize(Roles = "Crowdworker")]
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
        //[Authorize(Roles = "Crowdworker")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Create([Bind("GameID,GraphicQuality,Playability,StoryCharacterDevelopment," +
            "GameplayControls,Multiplayer,OverallRating,Pros,Cons,WrittenReview,Summary,DatePosted")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        //[Authorize(Roles = "Crowdworker")]
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
        //[Authorize(Roles = "Crowdworker")]
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
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        //[Authorize(Roles = "Crowdworker")]
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
        //[Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private double CalculateOverallRating(Review viewModel)
        {
            return (viewModel.GraphicQuality + viewModel.Playability
                    + viewModel.StoryCharacterDevelopment + viewModel.GameplayControls
                    + viewModel.Multiplayer) / 5.0;
        }
    }
}
