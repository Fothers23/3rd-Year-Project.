using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Game
    {
        [Key]
        [ScaffoldColumn(false)]
        public int GameID { get; set; }

        [Display(Name = "Game Logo/Cover")]
        public string Picture { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [StringLength(500)]
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
        [Range(1, 10)]
        public int NumberOfPlayers { get; set; }

        [Display(Name = "Available Platform")]
        public string AvailablePlatforms { get; set; }

        [Display(Name = "Reviews Required")]
        [Range(1,100)]
        public int ReviewQuantity { get; set; }
        
        [Display(Name = "Reward for review (£)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Range(0.01,20.00)]
        public decimal ReviewReward { get; set; }

        [Display(Name = "Budget (£)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Budget { get; set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
        
        public ApplicationUser Developer { get; set; }
    }
}