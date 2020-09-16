using Hangman.GameLogics;
using Hangman.Models;
using System;
using System.Collections.Generic;
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

        public TopMenuUC()
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;
        }

        public void PlayerStatusChanged(IPlayer player)
        {
            _vm.PlayerName = player?.Name;
        }

        #region MnuMethods
        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            ParentWindow.Main.Content = new LoginPage();
            PlayerStatusChanged(null);
        }

        private void mnuDeleteUser(object sender, RoutedEventArgs e)
        {
            ParentWindow.Main.Content = new DeleteUserPage(PlayerEngine.ActivePlayer);
        }

        private void mnuUpdateUser(object sender, RoutedEventArgs e)
        {
            ParentWindow.Main.Content = new UpdateUserPage(PlayerEngine.ActivePlayer);
        }
        #endregion
    }
}
