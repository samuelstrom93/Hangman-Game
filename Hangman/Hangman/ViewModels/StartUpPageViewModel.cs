using System;
using System.Collections.Generic;
using System.Text;
using Hangman.ViewModels.Base;
using Hangman.Modules;
using System.Windows.Input;


namespace Hangman.ViewModels
{
    class StartUpPageViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; set; } //Binding i StartUpPage.xaml

        public StartUpPageViewModel()
        {
            LogOutCommand = new RelayCommand(LogOut);

        }

        private void LogOut()
        {
            SetActivePlayer(null);
            GoToPage(ApplicationPage.Login);
        }
    }
}
