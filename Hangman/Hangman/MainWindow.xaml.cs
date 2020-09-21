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
            //model = new MainWindowViewModel();
            //TopMenuUser.Content = PlayerEngine._menu;
            //this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.8);
            //this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.8);

            //Main.Content = new LoginPage();

            DataContext = new MainWindowViewModel();
        }
    }
}

