﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        public string Image { get; set; }

        // Requester
        [StringLength(300)]
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal BudgetTotal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Spent { get; set; }

        [Display(Name = "My Games")]
        public List<Game> MyGames { get; set; }

        // Crowdworker
        [Display(Name = "My Rating")]
        public double Rating { get; set; }

        [Display(Name = "My Reviews")]
        public List<Review> MyReviews { get; set; }

        public ApplicationUser()
        {
            Image = "avatar.png";
        }
    }
}
