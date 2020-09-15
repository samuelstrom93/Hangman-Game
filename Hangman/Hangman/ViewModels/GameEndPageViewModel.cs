using Hangman.Models;
using Hangman.ViewModels.Base;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    class GameEndPageViewModel : BaseViewModel
    {
        public TimeSpan Timer { get; set; } = GamePageViewModel.Game.EndTime - GamePageViewModel.Game.StartTime;
        public string NumberOfCorrectTries { get; set; }

        public string NumberOfIncorrectTries { get; set; } = GamePageViewModel.Game.NumberOfIncorrectTries.ToString();

        public IWord Word { get; set; } = GamePageViewModel.IWord;




        public GameEndPageViewModel()
        {
            int nmbrTries = GamePageViewModel.Game.NumberOfTries - GamePageViewModel.Game.NumberOfIncorrectTries;
            NumberOfCorrectTries = nmbrTries.ToString();
        }
    }
}
