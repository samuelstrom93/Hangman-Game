using Hangman.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
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
    }
}
