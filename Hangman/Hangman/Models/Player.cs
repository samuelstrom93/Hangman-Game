using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    class Player : IPlayer
    {
        public string Name { get ; set; }
        public int Id { get; set; }
    }
}
