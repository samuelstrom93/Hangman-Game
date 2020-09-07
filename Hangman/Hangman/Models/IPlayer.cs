using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    interface IPlayer
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
