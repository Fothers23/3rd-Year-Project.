using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class GameList
    {
        public int NumberOfGames { get; set; }
        public List<Game> Games { get; set; }
    }
}
