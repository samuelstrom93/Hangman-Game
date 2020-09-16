using Hangman.GameLogics;
using Hangman.Repositories;
using Hangman.Views;
using Hangman.Views.Menu;
using Hangman.Views.PlayerStats;
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
            TopMenu.Content = PlayerEngine._menu;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.5);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.5);

            Main.Content = new LoginPage();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth);
            }

        //private void NotifyIconClickReset(object sender, RoutedEventArgs e)
        //{
        //    MenuShowName.Header = "Name";
        //}

        
    }
}

