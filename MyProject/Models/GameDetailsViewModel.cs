using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }

        public List<Review> Reviews { get; set; }

        public int ReviewID { get; set; }

        public int GameID { get; set; }

        [Display(Name = "Graphic Quality")]
        public double GraphicQuality { get; set; }

        public double Playability { get; set; }

        [Display(Name = "Story/Character Development")]
        public double StoryCharacterDevelopment { get; set; }

        [Display(Name = "Gameplay & Controls")]
        public double GameplayControls { get; set; }

        [Display(Name = "Co-op/Online Multiplayer")]
        public double Multiplayer { get; set; }

        [Display(Name = "Overall Rating")]
        public double OverallRating { get; set; }

        public string Pros { get; set; }

        public string Cons { get; set; }

        [Display(Name = "Written Review")]
        public string WrittenReview { get; set; }

        public string Summary { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }
    }
}
