using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    class GameSuccessPageViewModel
    {
        public IWord Word { get; set; } = GamePageViewModel.IWord;

        //public IGame Game { get; set; } = GamePageViewModel.GetGameHighscore();
 
        //public int Ranking { get; set; }




        public GameSuccessPageViewModel()
        {
        }
    }
}
