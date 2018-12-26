using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Review
    {
        [Key, ScaffoldColumn(false)]
        public int RId { get; set; }

        public int GraphicQuality { get; set; }

        public int Playability { get; set; }

        public int StoryCharacterDevelopment { get; set; }

        public int GameplayControls { get; set; }

        public int Multiplayer { get; set; }

        public string[] Pros { get; set; }

        public string[] Cons { get; set; }

        public string WrittenReview { get; set; }

        public string Summary { get; set; }
    }
}
