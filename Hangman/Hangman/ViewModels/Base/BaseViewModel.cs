using Hangman.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hangman.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected static NavigationService NavigationService { get; } = (Application.Current.MainWindow as MainWindow).Main.NavigationService;
        public ICommand NavigateToPageByParameterCommand { get; set; }


        protected virtual void GoToPage(object parameter)
        {
            var selectedPage = (ApplicationPage)parameter;
            Page page = NavigateToPageHelper.GetPage(selectedPage);
            NavigationService.Navigate(page);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public BaseViewModel()
        {
            NavigateToPageByParameterCommand = new RelayParameterizedCommand(parameter => GoToPage(parameter));
        }
    }
}
