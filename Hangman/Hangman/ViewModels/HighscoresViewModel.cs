using Hangman.Models;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscoresViewModel : BaseViewModel
    {
        public List<HighscoreGame> TopHighscores { get; set; }
        public List<HighscoreGame> TopCurrentPlayerHighscores { get; set; }
        public Dictionary<string, long> TopDiligentPlayers { get; set; }

        public string Title { get; set; }
    }
}
