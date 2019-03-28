using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Review
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ReviewID { get; set; }
        
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
        [DataType(DataType.MultilineText)]
        public string WrittenReview { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }

        [Display(Name = "Rate My Review")]
        public double ReviewRating { get; set; }

        // This constructor sets the current time when the review is posted.
        public Review()
        {
            DatePosted = DateTime.Now;
        }

        public Game Game { get; set; }

        public ApplicationUser User { get; set; }

    }
}