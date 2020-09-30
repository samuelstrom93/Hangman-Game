using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Repositories;
using Npgsql;
using Hangman.ViewModels.Base;
using System.Windows.Input;
using Hangman.Modules;
using Hangman.ViewModels;
using System.Windows;

namespace Hangman.ViewModels
{
    class CreateUserViewModel : BaseViewModel
    {

        #region Properties
        public string PlayerName { get; set; }
        public string Message { get; set; }
        public ICommand TryRegister { get; set; }

        private readonly IPlayerModule _module;
        public string TextBoxBackground { get; set; }
        #endregion

        public CreateUserViewModel()
        {
            _module = new PlayerModule();
            TryRegister = new RelayCommand(TryAddPlayer);
        }

        private void TryAddPlayer()
        {
            if (string.IsNullOrWhiteSpace(PlayerName) )
            {
                TextBoxBackground = "white";
                Message = "Du måste skriva något.";
                return;
            }

            else if (PlayerName.Contains(" "))
            {
                TextBoxBackground = "white";
                Message = "Ditt namn får inte innehålla mellanslag";
                return;
            }

            if (_module.TryAddPlayer(PlayerName, out _))
            {
                SetActivePlayer(PlayerName);
                MessageBox.Show($"Välkommen {ActivePlayerName}! Du är nu en medlem!");
                GoToPage(ApplicationPage.GamePage);
                return;
            }

            
            TextBoxBackground = "white";
            Message = "Du har valt ett namn som är upptaget - försök igen";
        }
    }
}

