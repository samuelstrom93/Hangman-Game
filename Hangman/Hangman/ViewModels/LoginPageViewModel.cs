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
        #region Properties
        public string ErrorMessage { get; set; }
        public string PlayerName { get; set; }
        public string MessageBackground { get; set; }
        private readonly IPlayerModule _module = new PlayerModule();
        #endregion

        #region Commands
        public ICommand TryLogIn { get; set; }
        #endregion

        public LoginPageViewModel()
        {
            TryLogIn = new RelayCommand(TryLogInPlayer);
        }

        #region Methods
        private void TryLogInPlayer()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                if (string.IsNullOrEmpty(PlayerName))
                {
                    SetErrorMessageDesign();
                     ErrorMessage = "Du måste skriva något";
                }

                else
                {
                    SetErrorMessageDesign();
                    ErrorMessage = "Du får inte använda mellanslag";
                }

                return;
            }

            if (PlayerName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                SetActivePlayer("Admin");
                GoToPage(ApplicationPage.Admin);
            }

            else if (!_module.TryLogInPlayer(PlayerName))
            {
                SetErrorMessageDesign();
                ErrorMessage = "Din användare finns inte!";
            }
            else
            {
                SetActivePlayer(PlayerName);
                GoToPage(ApplicationPage.GamePage);
            }
        }

        private void SetErrorMessageDesign()
        {
            PlayerName = null;
            MessageBackground = "white";

        }
        #endregion
    }
}
