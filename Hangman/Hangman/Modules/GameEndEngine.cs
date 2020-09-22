using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.GameRepository;
using static Hangman.Repositories.HighscoreRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Repositories;
using System.Windows.Controls;

namespace Hangman.Moduls
{
    class GameEndEngine : DefaultBaseViewModel
    {
        public IGame IGame { get; set; }
        public IWord IWord { get; set; }

        #region Properties: Binding i GameEndPage

        public Button Button { get; set; }

        public int NumberOfCorrectTries { get; set; }
        public int Ranking { get; set; }

        public string GameStatus { get; set; } 
        public string QuitBtnContent { get; set; }

        public bool IsRankingShown { get; set; }   
        public bool IsDeleteGameScoreBtnShown { get; set; } 

        #endregion

        public GameEndEngine(Game game, Word word)
        {
            SetIGame(game);
            SetGame(game);
            SetIWord(word);
            SetNumberOfCorrectTries();
            SetGameStatus();
            SetBtn();
            

            ChangeQuitBtnContent();
            SwitchRankingView();
        }


        #region Methods for Get+DeleteGame

        private Game game;
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

        private void SetIGame(Game game)
        {
            IGame = game;
        }

        private void SetIWord(Word word)
        {
            IWord = word;
        }

        private void SetNumberOfCorrectTries()
        {
            NumberOfCorrectTries = IGame.NumberOfTries - IGame.NumberOfIncorrectTries;
        }

        private void SetGameStatus()
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

        private void SetBtn()
        {
            Button = new Button();
            Button.Content = "Kasta bort min spelpoäng!";
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

        private void ChangeQuitBtnContent()
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

        public void ChangeBtnStyle()
        {
            Button.Opacity = 0.5;
            Button.Content = "Kastat!";
            Button.IsEnabled = false;
        }

        #endregion

    }
}
