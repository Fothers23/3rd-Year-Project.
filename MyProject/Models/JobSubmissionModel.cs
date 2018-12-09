using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class JobSubmissionModel
    {
        [Required, Key]
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Description { get; set; }
        [MinLength(1), MaxLength(3)]
        public string AgeRating { get; set; }
        public string Genre { get; set; }
        public int NumberOfPlayers { get; set; }
        public string AvailablePlatforms { get; set; }
    }
}
