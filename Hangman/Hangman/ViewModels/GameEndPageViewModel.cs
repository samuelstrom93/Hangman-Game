using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.Game_Repository;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    class GameEndPageViewModel : BaseViewModel
    {
        public ICommand SaveGameScoreCommand { get; set; }
        public IGame IGame { get; set; } 
        public IWord IWord { get; set; }  

        public int NumberOfCorrectTries { get; set; } //Binding i GameEnd_Page
        public string GameStatus { get; set; }  //Binding i GameEnd_Page
        public string QuitBtnContent { get; set; }  //Binding i GameEnd_Page

        public bool IsRankingShown { get; set; }
        public bool IsDeleteGameScoreBtnShown { get; set; }



        public GameEndPageViewModel(Game game, Word word)
        {
            SetIGame(game);
            SetGame(game);
            SetIWord(word);
            SetNumberOfCorrectTries();
            SetGameStatus();

            SwitchRankingView();
            DistinguishPlayer();

            SaveGameScoreCommand = new RelayCommand(DeleteGameScore);
        }

        private Game game { get; set; }
        public Game GetGame()
        {
            return game;
        }

        private void SetGame(Game playersGameScore)
        {
            game = playersGameScore;
        }


        private void DeleteGameScore()
        {
            DeleteGame(gameID);
        }


        #region Metoder
        public void SetIGame(Game game)
        {
            IGame = game;
        }

        public void SetIWord(Word word)
        {
            IWord = word;
        }

        public void SetNumberOfCorrectTries()
        {
            NumberOfCorrectTries = IGame.NumberOfTries - IGame.NumberOfIncorrectTries;
        }

        public void SetGameStatus()
        {
            if (IGame.IsWon == true)
            {
                GameStatus = "Du vann!";
            }
            else
            {
                GameStatus = "Du förlorade...";
            }
        }

        private void SwitchRankingView()
        {
            if ((IGame.IsWon == true)&&(game.PlayerId != 0))
            {
                IsRankingShown = true;
            }
            else
            {
                IsRankingShown = false;
            }
        }

        private int gameID;
        private void DistinguishPlayer()
        {
            if(game.PlayerId != 0)  // Spelaren med inloggning
            {
                gameID = AddGame(game);
                IsDeleteGameScoreBtnShown = true;
                QuitBtnContent = "Logga ut";
            }
            else
            {
                IsDeleteGameScoreBtnShown = false;
                QuitBtnContent = "Avsluta spel";
            }
        }

        #endregion
    }
}
