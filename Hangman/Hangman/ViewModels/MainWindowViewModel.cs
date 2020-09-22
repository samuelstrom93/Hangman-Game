using Hangman.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hangman.Views 
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } = "Meny";
        public Visibility Visibility { get; set; }

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
