using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.Game_Repository;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hangman.Repositories;

namespace Hangman.ViewModels
{
    class GameEndPageViewModel : BaseViewModel
    {
        #region public fields

        public ICommand DeleteGameScoreCommand { get; set; } //Binding i GameEnd_Page
        public IGame IGame { get; set; } 
        public IWord IWord { get; set; }  

        public int NumberOfCorrectTries { get; set; } //Binding i GameEnd_Page
        public string GameStatus { get; set; }  //Binding i GameEnd_Page
        public string QuitBtnContent { get; set; }  //Binding i GameEnd_Page

        public bool IsRankingShown { get; set; }    //Binding i GameEnd_Page
        public bool IsDeleteGameScoreBtnShown { get; set; }     //Binding i GameEnd_Page
        public int Ranking { get; set; }

        #endregion

        public GameEndPageViewModel(Game game, Word word)
        {
            SetIGame(game);
            SetGame(game);
            SetIWord(word);
            SetNumberOfCorrectTries();
            SetGameStatus();

            DistinguishPlayer();
            SwitchRankingView();
            
            DeleteGameScoreCommand = new RelayCommand(DeleteGameScore);
        }

        #region Methods for Get+DeleteGame

        private Game game { get; set; }
        public Game GetGame()
        {
            return game;
        }

        private void DeleteGameScore()
        {
            DeleteGame(gameID);
        }

        #endregion

        #region SetMethods

        private void SetGame(Game playersGameScore)
        {
            game = playersGameScore;
        }

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
        #endregion

        #region Methods for UI 
        private void SwitchRankingView()
        {
            if ((IGame.IsWon == true)&&(game.PlayerId != 0))
            {
                IsRankingShown = true;
                Ranking = HighscoreRepository.GetRankOnHighScore(game.Id);
            }
            else
            {
                IsRankingShown = false;
            }
        }

        private int gameID;

        private void DistinguishPlayer()
        {
            if(game.PlayerId != 0)  // Spelaren MED inloggning
            {
                gameID = AddGame(game);
                IsDeleteGameScoreBtnShown = true;
                QuitBtnContent = "Logga ut";
            }
            else   //Spelaren UTAN inloggning
            {
                IsDeleteGameScoreBtnShown = false;
                QuitBtnContent = "Avsluta spel";
            }
        }

        #endregion

    }
}
