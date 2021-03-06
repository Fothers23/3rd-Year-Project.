﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string Name { get; set; }

            public string Image { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            //Requester
            [StringLength(300)]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Company Description")]
            public string CompanyDescription { get; set; }

            [Display(Name = "Budget Total")]
            [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
            public decimal BudgetTotal { get; set; }

            [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
            public decimal Spent { get; set; }

            [Display(Name = "My Games")]
            public List<Game> MyGames { get; set; }

            //Crowdworker
            [Display(Name = "My Rating")]
            public double Rating { get; set; }

            [Display(Name = "My Reviews")]
            public List<Review> MyReviews { get; set; }
        }

        // Retrieves and displays user info from the database.
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            // Initializes myGames as a list of Games where the current user id matches the Game Developer id.
            var myGames = await _context.Games.Where(x => x.Developer.Id == user.Id).ToListAsync();
            // Initializes myReviews as a list of Reviews where the current user id matches the Review user id.
            var myReviews = await _context.Reviews.Where(x => x.User.Id == user.Id).Include(r => r.Game).ToListAsync();
            foreach (var item in myReviews)
            {
                user.Rating += item.ReviewRating / myReviews.Count();
            }
            foreach (var item in myGames)
            {
                user.BudgetTotal += item.Budget;
                user.Spent = item.ReviewReward * _context.Reviews.Where(x => x.Game.Developer.Id == user.Id).Count();
            }

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name,
                Image = user.Image,
                Email = email,
                CompanyDescription = user.CompanyDescription,
                BudgetTotal = user.BudgetTotal,
                Spent = user.Spent,
                MyGames = myGames,
                Rating = user.Rating,
                MyReviews = myReviews
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        // Updates the user info in the database if it is edited by the user.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.CompanyDescription != user.CompanyDescription)
            {
                user.CompanyDescription = Input.CompanyDescription;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                user.UserName = Input.Email;
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId, code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
