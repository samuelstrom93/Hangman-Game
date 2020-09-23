using Hangman.Modules;
using Hangman.Views;
using Hangman.Views.Menu;
using Hangman.Views.PlayGame;
using System.Windows;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TopMenu.Content = new TopMenuUC();

            Main.Content = new PlayGamePage();
            DataContext = new MainWindowViewModel();
        }
    }
}

