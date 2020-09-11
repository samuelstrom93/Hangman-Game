using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscoresViewModel
    {
        public List<HighscoreGame> TopHighscores { get; set; }
        public List<HighscoreGame> TopCurrentPlayerHighscores { get; set; }
        public Dictionary<string, long> TopDiligentPlayers { get; set; }
    }
}
