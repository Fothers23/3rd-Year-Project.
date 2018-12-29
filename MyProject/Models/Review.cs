using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Review
    {
        [Key, ScaffoldColumn(false)]
        public int RId { get; set; }

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
        public string WrittenReview { get; set; }

        public string Summary { get; set; }
    }
}
