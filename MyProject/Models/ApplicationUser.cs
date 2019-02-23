using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Requester
        [StringLength(60)]
        public string DeveloperName { get; set; }

        [StringLength(300)]
        public string CompanyDescription { get; set; }

        public List<Game> MyGames { get; set; }

        // Crowdworker
        public List<Review> MyReviews { get; set; }
    }
}
