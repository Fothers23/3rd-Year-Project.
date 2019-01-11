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
        public int GraphicQuality { get; set; }

        public int Playability { get; set; }

        [Display(Name = "Story/Character Development")]
        public int StoryCharacterDevelopment { get; set; }

        [Display(Name = "Gameplay & Controls")]
        public int GameplayControls { get; set; }

        [Display(Name = "Co-op/Online Multiplayer")]
        public int Multiplayer { get; set; }

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

        // This constructor sets the current time when the review is posted.
        public Review()
        {
            DatePosted = DateTime.Now;
        }

        public virtual Game MyGame { get; set; }
    }
}
