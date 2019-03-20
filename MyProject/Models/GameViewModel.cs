using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class GameViewModel
    {
        public Game Game { get; set; }

        public Review Review { get; set; }
    }
}
