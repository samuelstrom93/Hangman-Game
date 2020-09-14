using Hangman.GameLogics;
using Hangman.Repositories;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Main.Content = new LoginPage();
            this.SizeToContent = SizeToContent.Height;
        }


        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            Main.Content = new LoginPage();        
            
        }

        private void mnuPlay(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            Main.Content = new GamePage();

        }

        private void mnuUserSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new UserSettingsPage();
        }

    }
}

