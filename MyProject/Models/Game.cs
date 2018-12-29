using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Game
    {
        [Key, ScaffoldColumn(false)]
        public int GId { get; set; }

        [Required, MinLength(2), MaxLength(60)]
        public string Title { get; set; }

        [MinLength(2), MaxLength(60)]
        public string Developer { get; set; }

        [MinLength(2), MaxLength(150)]
        public string Description { get; set; }

        [Display(Name = "Age Rating")]
        [MinLength(1), MaxLength(3)]
        public string AgeRating { get; set; }

        [MinLength(2), MaxLength(60)]
        public string Genre { get; set; }

        [Display(Name = "Number of Players")]
        public int NumberOfPlayers { get; set; }

        [Display(Name = "Available Platforms")]
        public string AvailablePlatforms { get; set; }

        [Display(Name = "Number of reviews required")]
        public int ReviewQuantity { get; set; }

        [Display(Name = "Reward for review")]
        public string ReviewReward { get; set; }
    }
}
