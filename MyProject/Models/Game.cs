using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        [MinLength(1), MaxLength(3)]
        public string AgeRating { get; set; }

        [MinLength(2), MaxLength(60)]
        public string Genre { get; set; }

        public int NumberOfPlayers { get; set; }

        public string AvailablePlatforms { get; set; }

        public int ReviewQuantity { get; set; }

        public string ReviewReward { get; set; }
    }
}
