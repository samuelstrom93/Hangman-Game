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
        #region Properties
        public string LogBtnContent { get; set; }
        public bool IsCreateUserButtonEnabled { get; set; } = true;
        public string IsCreateUserButtonVisible { get; set; }
        #endregion

        #region Commands
        public ICommand LoginOrLogOutCommand { get; set; } //Binding i StartUpPage.xaml
        public ICommand CreateUserCommand { get; set; }
        #endregion
        public StartUpPageViewModel()
        {
            LoginOrLogOutCommand = new RelayCommand(LogInOrLogOut);
            CreateUserCommand = new RelayCommand(CreateUser);
            SetButton();
        }

        private void LogInOrLogOut()
        {
            if(ActivePlayer == null)
            {               
                GoToPage(ApplicationPage.Login);
            }

            else
            {
                SetActivePlayer(null);
                GoToPage(ApplicationPage.StartUpPage);
            }
        }

        private void CreateUser()
        {
            GoToPage(ApplicationPage.CreateUser);
        }

        private void SetButton()
        {
            if (ActivePlayer == null)
            {
                LogBtnContent = "LOGGA IN";
            }

            else
            {
                LogBtnContent = "LOGGA UT";
                IsCreateUserButtonEnabled = false;
                IsCreateUserButtonVisible = "hidden";
            }

        }
    }
}
