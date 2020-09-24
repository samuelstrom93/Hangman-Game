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
        public ObservableCollection<object> ItemsToShow { get; set; } = new ObservableCollection<object>();

        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

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
                    ItemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                    ItemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                    ItemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));

                }
                else
                {
                    ItemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                    ItemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                    ItemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));
;
                }
            }
        }

        private void CreateMenuItems()
        {
            var loginItem = new MenuItem
            {
                Name = "LoginItem",
                Header = "Logga in"
            };
            loginItem.Click += mnuLogin;
            _menuItems.Add(loginItem);

            var logoutItem = new MenuItem
            {
                Name = "LogoutItem",
                Header = "Logga ut"
            };
            logoutItem.Click += mnuLogOut;
            _menuItems.Add(logoutItem);

            var userSettingsPage = new MenuItem
            {
                Name = "UserSettingsItem",
                Header = "Användarinställningar"
            };
            userSettingsPage.Click += mnuUserSettings;
            _menuItems.Add(userSettingsPage);

            var playItem = new MenuItem
            {
                Name = "PlayItem",
                Header = "Spela"
            };

            playItem.Click += mnuPlay;
            _menuItems.Add(playItem);

            var highScoreItem = new MenuItem
            {
                Name = "HighScoresItem",
                Header = "Topplistor"
            };
            highScoreItem.Click += mnuHighScores;
            _menuItems.Add(highScoreItem);


            ItemsToShow.Add(loginItem);
            ItemsToShow.Add(highScoreItem);
            ItemsToShow.Add(new Separator());
            ItemsToShow.Add(playItem);
        }

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            SetActivePlayer(null);
            GoToPage(ApplicationPage.Login);
        }

        private void mnuUserSettings(object sender, RoutedEventArgs e)
        {
            GoToPage(ApplicationPage.UserSettings);
        }
        private void mnuPlay(object sender, RoutedEventArgs e)
        {
            GoToPage(ApplicationPage.GamePage);
        }
        private void mnuLogin(object sender, RoutedEventArgs e)
        {
            GoToPage(ApplicationPage.Login);
        }

        private void mnuHighScores(object sender, RoutedEventArgs e)
        {
            GoToPage(ApplicationPage.HighscorePage);
        }
    }
}
