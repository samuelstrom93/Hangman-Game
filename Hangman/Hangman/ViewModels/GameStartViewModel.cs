using Hangman.Modules;
using Hangman.ViewModels.Base;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class GameStartViewModel : BaseViewModel
    {
        public Visibility Visibility { get; set; }
        public ICommand PlayGame { get; set; }
        public ICommand GoToGameGuide { get; set; }
        public TopGamesUC Highscores { get; set; }

        public GameStartViewModel()
        {
            var playerId = PlayerModule.GetActivePlayer()?.Id;
            Highscores = new TopGamesUC(playerId);

            PlayGame = new RelayCommand(PlayGameCommand);
            GoToGameGuide = new RelayCommand(NavigateToGameGuide);
        }

        private void PlayGameCommand()
        {
            Visibility = Visibility.Collapsed;
        }

        private void NavigateToGameGuide()
        {
            //TODO: Samuels navigation grej
        }
    }
}
