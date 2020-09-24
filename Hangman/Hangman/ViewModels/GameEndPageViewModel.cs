using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.PlayerRepository;
using static Hangman.Repositories.GameRepository;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hangman.Repositories;
using Hangman.Moduls;
using System.Windows.Controls;
using Hangman.Views;

namespace Hangman.ViewModels
{
    class GameEndPageViewModel : BaseViewModel
    {
        public ICommand PlayAgainCommand { get; set; }  //Binding i GameEnd_Page
        public ICommand DeleteGameScoreCommand { get; set; } //Binding i GameEnd_Page
        public ICommand LogOutCommand { get; set; } //Binding i GameEnd_Page

        public GameEndEngine GameEndEngine { get; set; }

        private PlayerRepository playerRepository { get; set; }

        public GameEndPageViewModel(Game game, Word word)
        {
            GameEndEngine = new GameEndEngine(game, word);

            PlayAgainCommand = new RelayCommand(PlayAgain);
            DeleteGameScoreCommand = new RelayCommand(DeleteGameScore);
            LogOutCommand = new RelayCommand(LogOut);

            playerRepository = new PlayerRepository();
        }


        private void PlayAgain()
        {
            bool isPlayAgain = true;
            
            if (GameEndEngine.GetGame().PlayerId != 0)    // Behåller inloggning
            {
                var page = new GamePage(playerRepository.GetPlayerFromID(GameEndEngine.GetGame().PlayerId), isPlayAgain);
                NavigationService.Navigate(page);
            }

            else
            {
                var page = new GamePage(isPlayAgain: isPlayAgain);
                NavigationService.Navigate(page);
            }
        }

        private void DeleteGameScore()
        {
            GameEndEngine.DeleteGameScore();
            GameEndEngine.ChangeBtnStyle();
        }

        private void LogOut()
        {
            SetActivePlayer(null);
            GoToPage(ApplicationPage.Login);
        }


    }
}
