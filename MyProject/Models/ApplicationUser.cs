using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        // Requester
        [StringLength(300)]
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }

        public double Budget { get; set; }

        public double Spent { get; set; }

        public List<Game> MyGames { get; set; }

        // Crowdworker
        public double Rating { get; set; }

        public List<Review> MyReviews { get; set; }
    }
}
