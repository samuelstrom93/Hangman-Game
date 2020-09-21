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
        public ICommand GoToCreateUser { get; set; }
        public ICommand GoToGameIntro { get; set; }
        public ICommand PlayWithoutUser { get; set; }
        public ICommand TryLogIn { get; set; }

        private readonly PlayerModule _module = new PlayerModule();


        public LoginPageViewModel()
        {
            GoToCreateUser = new RelayCommand(NavigateToCreateUser);
            GoToGameIntro = new RelayCommand(NavigateToGameIntro);
            PlayWithoutUser = new RelayCommand(QuickPlay);
            TryLogIn = new RelayCommand(TryLogInPlayer);
        }

        private void NavigateToCreateUser()
        {
            //TODO
        }

        private void NavigateToGameIntro()
        {
            //TODO
        }

        private void QuickPlay()
        {
            //TODO
        }

        private void TryLogInPlayer()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                return;
            }

            if (PlayerName.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                //TODO navigate admin
            }

            if (!_module.TryLogInPlayer(PlayerName))
            {
                ErrorMessage = "Din användare finns inte!";
            }
            else
            {
                //TODO NAVIGATION
            }
        }
    }
}
