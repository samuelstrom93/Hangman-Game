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
        public IGame Game { get; set; } 
        public IWord Word { get; set; }  

        public int NumberOfCorrectTries { get; set; } //Binding i GameEnd_Page
        public string GameStatus { get; set; }  //Binding i GameEnd_Page

        public bool IsRankingShown { get; set; }

        public GameEndPageViewModel(Game game, Word word)
        {
            SetGame(game);
            SetWord(word);
            SetNumberOfCorrectTries();
            SetGameStatus();
            SwitchRanking();
        }


        #region Metoder
        public void SetGame(Game game)
        {
            Game = game;
        }

        public void SetWord(Word word)
        {
            Word = word;
        }

        public void SetNumberOfCorrectTries()
        {
            NumberOfCorrectTries = Game.NumberOfTries - Game.NumberOfIncorrectTries;
        }

        public void SetGameStatus()
        {
            if (Game.IsWon == true)
            {
                GameStatus = "Du vann!";
            }
            else
            {
                GameStatus = "Du förlorade...";
            }
        }

        private void SwitchRanking()
        {
            if (Game.IsWon == true)
            {
                IsRankingShown = true;
            }
            else
            {
                IsRankingShown = false;
            }
        }

        #endregion
    }
}
