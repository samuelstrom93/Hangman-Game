using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hangman.ViewModels
{
    public class TopMenuViewModel : BaseViewModel
    {
        public string PlayerName { get; set; } = "MENY";
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; } = new ObservableCollection<MenuItemViewModel>();

        private readonly List<MenuItemViewModel> _menuItems = new List<MenuItemViewModel>();

        public TopMenuViewModel()
        {
            GlobalPropertyChanged += PlayerStatusChanged;
            CreateMenuItems();
        }

        public void PlayerStatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ActivePlayerName)))
            {
                PlayerName = ActivePlayerName ?? "Meny";

                if (!string.IsNullOrWhiteSpace(ActivePlayerName))
                {
                    MenuItems.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                    MenuItems.Add(_menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));
                    MenuItems.Add(_menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                }
                else
                {
                    MenuItems.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                    MenuItems.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));
                    MenuItems.Add(_menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                }
            }
        }

        private void CreateMenuItems()
        {
            var loginItem = new MenuItemViewModel
            {
                Name = "LoginItem",
                Header = "Logga in",
                Command = new RelayCommand(MnuLogin)
            };
            _menuItems.Add(loginItem);

            var logoutItem = new MenuItemViewModel
            {
                Name = "LogoutItem",
                Header = "Logga ut",
                Command = new RelayCommand(MnuLogOut)
            };
            _menuItems.Add(logoutItem);

            var userSettingsPage = new MenuItemViewModel
            {
                Name = "UserSettingsItem",
                Header = "Användarinställningar",
                Command = new RelayCommand(MnuUserSettings)
            };
            _menuItems.Add(userSettingsPage);

            var playItem = new MenuItemViewModel
            {
                Name = "PlayItem",
                Header = "Spela",
                Command = new RelayCommand(MnuPlay)
            };
            _menuItems.Add(playItem);

            var highScoreItem = new MenuItemViewModel
            {
                Name = "HighScoresItem",
                Header = "Topplistor",
                Command = new RelayCommand(MnuHighScores)
            };
            _menuItems.Add(highScoreItem);

            var startUpItem = new MenuItemViewModel
            {
                Name = "StartUpItem",
                Header = "Startsida",
                Command = new RelayCommand(MnuStartUp)
            };
            _menuItems.Add(startUpItem);


            MenuItems.Add(playItem);
            MenuItems.Add(startUpItem);
            MenuItems.Add(highScoreItem);
            MenuItems.Add(loginItem);
        }

        private void MnuLogOut()
        {
            SetActivePlayer(null);
            GoToPage(ApplicationPage.StartUpPage);
        }

        private void MnuUserSettings()
        {
            GoToPage(ApplicationPage.UserSettings);
        }
        private void MnuPlay()
        {
            GoToPage(ApplicationPage.GamePage);
        }
        private void MnuLogin()
        {
            GoToPage(ApplicationPage.Login);
        }

        private void MnuHighScores()
        {
            GoToPage(ApplicationPage.HighscorePage);
        }

        private void MnuStartUp()
        {
            GoToPage(ApplicationPage.StartUpPage);
        }
    }
}
