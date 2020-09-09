using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    public class HighscoreGame : IGame
    {
        public string PlayerName { get; set; }
        public string Word { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfIncorrectTries { get; set; }
        public int NumberOfTries { get; set; }
        public bool IsWon { get; set; }
    }
}
