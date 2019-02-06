using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Crowdworker : IdentityUser
    {
        public List<Review> MyReviews { get; set; }
    }
}
