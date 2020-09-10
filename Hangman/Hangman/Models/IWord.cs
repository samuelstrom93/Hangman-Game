using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    interface IWord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hint { get; set; }
    }
}
