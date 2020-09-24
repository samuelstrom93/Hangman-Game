using Hangman.Helper;
using Hangman.Models;
using Hangman.Repositories;
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
        public ICommand NavigateBackParameterCommand { get; set; }

        private static string activePlayerName;
        public string ActivePlayerName 
        {
            get => activePlayerName;
            set
            {
                activePlayerName = value;
                OnGlobalPropertyChanged();
            }
        }

        protected static IPlayer ActivePlayer { get; private set; }

        protected void SetActivePlayer(string name)
        {
            PlayerRepository playerRepository = new PlayerRepository();
            ActivePlayer = string.IsNullOrWhiteSpace(name) ? null : playerRepository.GetPlayer(name);
            ActivePlayerName = name;
        }

        protected virtual void GoToPage(object parameter)
        {
            var selectedPage = (ApplicationPage)parameter;
            Page page = NavigateToPageHelper.GetPage(selectedPage);
            NavigationService.Navigate(page);
        }

        protected void GoBack()
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public static event PropertyChangedEventHandler GlobalPropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        protected static void OnGlobalPropertyChanged([CallerMemberName] string name = null)
        {
            GlobalPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        protected BaseViewModel()
        {
            NavigateToPageByParameterCommand = new RelayParameterizedCommand(parameter => GoToPage(parameter));
            NavigateBackParameterCommand = new RelayCommand(GoBack);
        }
    }
}
