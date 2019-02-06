using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Requester : IdentityUser
    {
        [Required]
        [StringLength(60)]
        public string DeveloperName { get; set; }

        [StringLength(300)]
        public string CompanyDescription { get; set; }

        public List<Game> MyGames { get; set; }
    }
}
