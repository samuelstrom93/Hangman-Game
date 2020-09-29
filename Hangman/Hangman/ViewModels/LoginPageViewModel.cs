using Hangman.Modules;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {        
        public string ErrorMessage { get; set; }
        public string PlayerName { get; set; }
        public ICommand TryLogIn { get; set; }

        public string MessageBackground { get; set; }

        private readonly IPlayerModule _module = new PlayerModule();


        public LoginPageViewModel()
        {
            TryLogIn = new RelayCommand(TryLogInPlayer);
        }

        private void TryLogInPlayer()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                if (string.IsNullOrEmpty(PlayerName))
                {
                     MessageBackground = "white";
                     ErrorMessage = "Du måste skriva något";
                }

                else
                {
                    ErrorMessage = "Du får inte använda mellanslag";
                    MessageBackground = "white";
                }

                return;
            }

            if (PlayerName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                GoToPage(ApplicationPage.Admin);
            }

            else if (!_module.TryLogInPlayer(PlayerName))
            {
                MessageBackground = "white";
                ErrorMessage = "Din användare finns inte!";
            }
            else
            {
                SetActivePlayer(PlayerName);
                GoToPage(ApplicationPage.GamePage);
            }
        }
    }
}
