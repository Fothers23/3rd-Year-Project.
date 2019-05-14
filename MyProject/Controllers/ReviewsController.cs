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

        /* Retrieves and creates a list of all reviews of the 
         * Game matching the id from the 
         * database and returns the corresponding Index View. 
        */
        public async Task<IActionResult> Index(int id, string sortOrder)
        {
            // Retrieves action input by user.
            ViewData["RatingSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var reviews = from r in _context.Reviews.Where(x => x.Game.GameID == id)
                           select r;

            // Changes the list order.
            switch (sortOrder)
            {
                case "name_desc":
                    reviews = reviews.OrderByDescending(r => r.OverallRating);
                    break;
                case "Date":
                    reviews = reviews.OrderBy(r => r.DatePosted);
                    break;
                case "date_desc":
                    reviews = reviews.OrderByDescending(r => r.DatePosted);
                    break;
                default:
                    reviews = reviews.OrderBy(r => r.OverallRating);
                    break;
            }
            return View(await reviews.AsNoTracking().ToListAsync());
        }

        /* Retrieves the Details view and populates it with 
         * Review details of the Review with the corresponding id.
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // Retrieves the Create View passing in the Game Id.
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Create(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.GameID == gameId);
            return View(new Review { Game = game });
        }

        // Deals with the input information from the Create View.
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
                    // Assigns user as Reviewer of the Game.
                    var user = await _userManager.GetUserAsync(User);
                    review.User = user;
                }
                var game = await _context.Games.FirstOrDefaultAsync(x => x.GameID == review.Game.GameID);
                review.Game = game; // Sets Game as the Game passed to the Get method.
                review.OverallRating = CalculateOverallRating(review);
                review.DatePosted = DateTime.Now; // Sets the DatePosted as the current date and time.

                _context.Add(review); // Adds Review to the database.
                await _context.SaveChangesAsync(); // Saves changes to the database.
                // Redirects the user to Review Index of the specific Game upon submission.
                return RedirectToAction(nameof(Index), new { id = review.Game.GameID } ); 
            }
            return View(review); // Returns the Create view if info input incorrectly.
        }

        /* Retrieves the Edit view and populates it with 
         * Review details of the Review with the corresponding id.
         */
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

        // Deals with the input information from the Edit View.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewID,GameID,GraphicQuality,Playability,StoryCharacterDevelopment," +
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

                    _context.Update(review); // Updates the database entry.
                    await _context.SaveChangesAsync(); // Saves changes to the database.
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
                // Redirects the user to Review Details upon submission.
                return RedirectToAction(nameof(Details), new { id = review.ReviewID });
            }
            return View(review); // Returns the Edit view if info input incorrectly.
        }

        /* Retrieves the Delete view and populates it with 
         * Review details of the Review with the corresponding id.
         */
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

        // Deals with the input information from the Delete view.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Crowdworker")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Finds the Review in the database.
            var review = await _context.Reviews.Include(r => r.Game).FirstOrDefaultAsync(x => x.ReviewID == id);
            _context.Reviews.Remove(review); // Deletes the Review from the database.
            await _context.SaveChangesAsync(); // Save changes to the database.
            // Redirects the user to Review Index of the specific Game upon submission.
            return RedirectToAction(nameof(Index), new { id = review.Game.GameID });
        }

        // Checks the id matches the id of a Review in the database.
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }

        // Calculates the average rating from the 5 input quality ratings.
        private double CalculateOverallRating(Review viewModel)
        {
            return (viewModel.GraphicQuality + viewModel.Playability
                    + viewModel.StoryCharacterDevelopment + viewModel.GameplayControls
                    + viewModel.Multiplayer) / 5.0;
        }

        // Retrieves the Rate view.
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

        // Deals with the input information from the Rate View.
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
                    _context.Update(review); // Updates the database entry.
                    await _context.SaveChangesAsync(); // Saves changes to the database.
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
                // Redirects the user to Review Details upon submission.
                return RedirectToAction(nameof(Details), new { id = review.ReviewID});
            }
            return View(review); // Returns the Rate view if info input incorrectly.
        }
    }
}
