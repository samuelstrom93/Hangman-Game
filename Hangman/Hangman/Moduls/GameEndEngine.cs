using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.GameRepository;
using static Hangman.Repositories.HighscoreRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Repositories;

namespace Hangman.Moduls
{
    class GameEndEngine : BaseViewModel
    {
        public IGame IGame { get; set; }
        public IWord IWord { get; set; }

        public int NumberOfCorrectTries { get; set; } //Binding i GameEnd_Page
        public string GameStatus { get; set; }  //Binding i GameEnd_Page
        public string QuitBtnContent { get; set; }  //Binding i GameEnd_Page

        public bool IsRankingShown { get; set; }    //Binding i GameEnd_Page
        public bool IsDeleteGameScoreBtnShown { get; set; }     //Binding i GameEnd_Page
        public int Ranking { get; set; }

        public GameEndEngine(Game game, Word word)
        {
            SetIGame(game);
            SetGame(game);
            SetIWord(word);
            SetNumberOfCorrectTries();
            SetGameStatus();

            DistinguishPlayer();
            SwitchRankingView();
        }

        #region Methods for Get+DeleteGame

        private Game game { get; set; }
        public Game GetGame()
        {
            return game;
        }
        private int gameID;
        public void DeleteGameScore()
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
            if ((IGame.IsWon == true) && (game.PlayerId != 0))    // Spelaren MED inloggning har vunnit
            {
                IsRankingShown = true;
                Ranking = HighscoreRepository.GetRankOnHighScore(game.Id);
            }
            else
            {
                IsRankingShown = false;
            }
        }


        private void DistinguishPlayer()
        {
            if (game.PlayerId != 0)  // Spelaren MED inloggning
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
