using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Hangman.Repositories;
using Npgsql;
using Hangman.ViewModels.Base;
using System.Windows.Input;
using Hangman.Modules;

namespace Hangman.ViewModels
{
    class CreateUserViewModel : BaseViewModel
    {
        public string PlayerName { get; set; }
        public string Message { get; set; }

        public ICommand TryRegister { get; set; }

        private readonly IPlayerModule _module;

        public CreateUserViewModel()
        {
            _module = new PlayerModule();

            TryRegister = new RelayCommand(TryAddPlayer);
        }

        private void TryAddPlayer()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                Message = "Du måste skriva något.";
                return;
            }

            if (_module.TryAddPlayer(PlayerName, out _))
            {
                Message = $"Grattis {PlayerName}! Du är nu medlem!";
                GoToPage(ApplicationPage.GamePage);
                return;
            }

            Message = "Du har valt ett namn som är upptaget - försök igen";
        }
    }
}

