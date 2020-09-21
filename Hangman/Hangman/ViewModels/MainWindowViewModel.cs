using Hangman.GameLogics;
using Hangman.Helper;
using Hangman.Models;
using Hangman.ViewModels;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } = "Meny";
        public Visibility Visibility { get; set; }

        #region Navigation
        public ICommand NavigateToPageCommand { get; set; }
        public MainWindowViewModel()
        {
            NavigateToPageCommand = new RelayCommand(GoToPage);
        }

        private void GoToPage()
        {
            Page page = new LoginPage();
            NavigationService.Navigate(page);
        }

        protected override void GoToPage(object parameter)
        {
            var selectedPage = (ApplicationPage)parameter;

            //var player = new Cpu { Fullname = "R2D2" };
            //var model = new Page2ViewModel(player); //Helt ok, eftersom Cpu implementerar IFullname


            var player = PlayerEngine.ActivePlayer;
            var model = new GamePageViewModel(player);
            Page page = NavigateToPageHelper.GetPage(selectedPage, model);
            NavigationService.Navigate(page);
        }

        #endregion
        public void UpdateActivePlayer()
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
        }
    }
}
