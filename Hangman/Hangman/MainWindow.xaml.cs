using Hangman.Modules;
using Hangman.Views;
using Hangman.Views.Menu;
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
            TopMenu.Content = TopMenuUC.Instance;

            Main.Content = new LoginPage();
            DataContext = new MainWindowViewModel();
        }
    }
}

