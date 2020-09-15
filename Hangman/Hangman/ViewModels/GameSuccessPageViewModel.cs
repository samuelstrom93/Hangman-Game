using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    class GameSuccessPageViewModel
    {
        public TimeSpan Timer { get; set; } = GamePageViewModel.Game.EndTime - GamePageViewModel.Game.StartTime;
        public string NumberOfCorrectTries { get; set; }

        public string NumberOfIncorrectTries { get; set; } = GamePageViewModel.Game.NumberOfIncorrectTries.ToString();

        public IWord Word { get; set; } = GamePageViewModel.IWord;

        //public int Ranking { get; set; }




        public GameSuccessPageViewModel()
        {
            int nmbrTries = GamePageViewModel.Game.NumberOfTries - GamePageViewModel.Game.NumberOfIncorrectTries;
            NumberOfCorrectTries = nmbrTries.ToString();

        }
    }
}
