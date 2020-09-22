using Hangman.Modules;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman.Views.Menu
{
    /// <summary>
    /// Interaction logic for TopMenuUC.xaml
    /// </summary>
    public partial class TopMenuUC : UserControl
    {
        private readonly MainWindowViewModel _vm;
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        private ObservableCollection<object> itemsToShow = new ObservableCollection<object>();

        private MainWindow _parentWindow;
        private MainWindow ParentWindow 
        { 
            get 
            {
                if (_parentWindow == null)
                {
                    _parentWindow = (MainWindow)Window.GetWindow(this);
                }

                return _parentWindow;
            }
        }

        private static TopMenuUC _instance;
        public static TopMenuUC Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TopMenuUC();
                }

                return _instance;
            }
        }

        private TopMenuUC()
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;

            CreateMenuItems();

            MenuShowName.ItemsSource = itemsToShow;
        }

        public void PlayerStatusChanged(IPlayer player)
        {
            _vm.PlayerName = player?.Name ?? "Meny";
            if (player != null)
            {
                itemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                itemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                itemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));
            }
            else
            {
                itemsToShow.Insert(0, _menuItems.SingleOrDefault(o => o.Name.Equals("LoginItem")));
                itemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("LogoutItem")));
                itemsToShow.Remove(_menuItems.SingleOrDefault(o => o.Name.Equals("UserSettingsItem")));
            }
        }

        #region 

        public void CreateMenuItems()
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
                Header = "Spela!"
            };
            playItem.Click += mnuPlay;
            _menuItems.Add(playItem);

            itemsToShow.Add(loginItem);
            itemsToShow.Add(new Separator());
            itemsToShow.Add(playItem);
        }

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            ParentWindow.Main.Content = new LoginPage();
            PlayerStatusChanged(null);
        }

        private void mnuUserSettings(object sender, RoutedEventArgs e)
        {
            ParentWindow.Main.Content = new UserSettingsPage();
        }
        private void mnuPlay(object sender, RoutedEventArgs e)
        {
            ParentWindow.Main.Content = new GamePage();
        }
        private void mnuLogin(object sender, RoutedEventArgs e)
        {
            ParentWindow.Main.Content = new LoginPage();
        }


        #endregion
    }
}
