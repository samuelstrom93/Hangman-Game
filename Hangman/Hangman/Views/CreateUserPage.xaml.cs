
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
using Hangman.ViewModels.Base;

namespace Hangman.Views
{
    /// <summary>
    /// CreateUser_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class CreateUser_Page : Page
    {
        private CreateUserViewModel model;
        private string message;
        private bool isPlayAgain = false;

        public CreateUser_Page()
        {
            InitializeComponent();
            //model = new CreateUserViewModel();
            DataContext = new CreateUserViewModel();
        }

        public CreateUser_Page(BaseViewModel specificModel)
        {
            InitializeComponent();
            DataContext = specificModel;
        }



        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {

            string name = txtBoxWantedName.Text;
            txtBoxWantedName.Clear();

            //kolla upp whitespaces
            if(name != "")
            {
                if (PlayerEngine.IsNameUsed(name) == false)
                {
                    message = model.CreatePlayer(name);                                      
                    PlayerEngine.SetActivePlayer(name);
                    this.NavigationService.Content = new GamePage(PlayerEngine.ActivePlayer, isPlayAgain);
                }

                else
                {
                    message = model.CreatePlayer(name);
                }

            }

            else 
            {
                message = "Du måste ange ett namn";
            }

            MessageBox.Show(message);
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
