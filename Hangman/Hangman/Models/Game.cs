using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    class Game
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfIncorrectTries { get; set; }
        public int NumberOfTries { get; set; }        
        public bool IsWon { get; set; }

        public int PlayerId { get; set; }
        public int WordId { get; set; }

    }
}
