
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
    /// CreateUser_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class CreateUser_Page : Page
    {
        private CreateUserViewModel model;

        public CreateUser_Page()
        {
            InitializeComponent();
            model = new CreateUserViewModel();
            DataContext = model;
        }


        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {

            string name = txtBoxWantedName.Text;
            txtBoxWantedName.Clear();

            if (PlayerEngine.IsNameUsed(name) == false)
            {
                model.CreatePlayer(name);
                PlayerEngine.ActivePlayer = Player_Repository.GetPlayer(name);
                this.NavigationService.Content = new GamePage(PlayerEngine.ActivePlayer);
            }

            else
            {
                model.CreatePlayer(name);
            }
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Content = new GameIntroPage();
        }

        private void Button_BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
