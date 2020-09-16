using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    class GameSuccessPageViewModel
    {
        public void SetGame(Game game)
        {
            Game = game;
        }

        public void GetWord(Word word)
        {
            Word = word;
        }

        public void SetNumberOfCorrectTries()
        {
            NumberOfCorrectTries = Game.NumberOfTries - Game.NumberOfIncorrectTries;
        }

        public IGame Game { get; set; }
        public IWord Word { get; set; }

        public int NumberOfCorrectTries { get; set; }


        public GameSuccessPageViewModel()
        {
        }
    }
}
