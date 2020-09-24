using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Moduls.InterfacesForDatabase
{
    interface IWordRepository
    {
        Word GetRandomWord();
        bool TryAddWord(string word, string hint, out Word addedWord);
    }
}
