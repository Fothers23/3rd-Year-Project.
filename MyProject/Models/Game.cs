using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Game
    {
        [Key, ScaffoldColumn(false)]
        public int GameID { get; set; }

        [Required, StringLength(60)]
        public string Title { get; set; }

        [StringLength(60)]
        public string Developer { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url]
        [Display(Name = "Game Link")]
        public string GameLink { get; set; }

        [Display(Name = "Age Rating")]
        [StringLength(3)]
        public string AgeRating { get; set; }

        [StringLength(60)]
        public string Genre { get; set; }

        [Display(Name = "Number of Players")]
        public int NumberOfPlayers { get; set; }

        [Display(Name = "Available Platforms")]
        public string AvailablePlatforms { get; set; }

        [Display(Name = "Reviews Required")]
        public int ReviewQuantity { get; set; }

        [Display(Name = "Reward for review")]
        public string ReviewReward { get; set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }

        // This constructor sets the current time when the Game is posted.
        public Game()
        {
            DatePosted = DateTime.Now;
        }
    }
}
