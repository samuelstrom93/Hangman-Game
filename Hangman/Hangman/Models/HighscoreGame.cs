using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    public class HighscoreGame
    {
        public string PlayerName { get; set; }
        public string Word { get; set; }
        public int NumberOfIncorrectTries { get; set; }
        public int NumberOfTries { get; set; }
        public TimeSpan GameTime { get; set; }
    }
}
