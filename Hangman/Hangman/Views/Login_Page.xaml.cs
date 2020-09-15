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
using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginPageViewModel model;
        private MainWindowViewModel MWmodel;

        public LoginPage()
        {
            InitializeComponent();
            model = new LoginPageViewModel();
            MWmodel = new MainWindowViewModel();
            DataContext = model;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //INPUT
            if (PlayerEngine.IsNameUsed(txtBoxUserInput.Text))
            {
                PlayerEngine.SetActivePlayer(txtBoxUserInput.Text);
                this.NavigationService.Content = new GamePage(PlayerEngine.ActivePlayer);
            }
            else
            {
                MessageBox.Show("Din användare finns inte!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var w = new HelpWindow();
            w.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new CreateUser_Page();
        }

        private void PlayWithoutUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GamePage();
        }
    }
}
