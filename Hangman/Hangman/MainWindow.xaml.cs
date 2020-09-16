using Hangman.GameLogics;
using Hangman.Views;

using System.Windows;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new MainWindowViewModel();
            TopMenu.Content = PlayerEngine._menu;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.7);

            Main.Content = new LoginPage();
        }

        //private void NotifyIconClickReset(object sender, RoutedEventArgs e)
        //{
        //    MenuShowName.Header = "Name";
        //}


    }
}

