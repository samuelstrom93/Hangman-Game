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
using Hangman.Views.ErrorMessage;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginPageViewModel model;
        private bool isPlayAgain = false;
        private BaseViewModel model1;

        public LoginPage()
        {
            InitializeComponent();
            //model = new LoginPageViewModel();
            //DataContext = model;
            DataContext = new LoginPageViewModel();
          
        }

        public LoginPage(BaseViewModel specificModel)
        {
            InitializeComponent();
            DataContext = specificModel;
        }

        #region Metods: ButtonClick

        /// <summary>
        /// Event som triggas vid tryck på "Logga in":  * Kontroll om användaren finns
        ///                                             * Kontroll om admin/vanlig spelare
        ///                                             * Logga in på rätt sida
        /// </summary>

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoginPossible())
            {
                if (CheckIfAdmin())
                    LoginAdmin();
                else
                    LoginPlayer();
            }
            else
            {
                ErrorFrame.Content = new ErrorMessageUC();
                model.ErrorMessage= "Din användare finns inte!";
                txtBoxUserInput.Clear();
            }


            DataContext = model;
        }
        #endregion

        #region Metods: Login player/admin

        public void LoginPlayer()
        {
            PlayerEngine.SetActivePlayer(txtBoxUserInput.Text);
            this.NavigationService.Content = new GamePage(PlayerEngine.ActivePlayer, isPlayAgain);
        }

        public void LoginAdmin()
        {
            this.NavigationService.Content = new AdminPage();
        }

        public bool IsLoginPossible()
        {
            if (PlayerEngine.IsNameUsed(txtBoxUserInput.Text))
                return true;

            else
                return false;
        }

        public bool CheckIfAdmin()
        {
            if (txtBoxUserInput.Text == "Admin")
                return true;
            else
                return false;
        }
        #endregion

        #region Methods: Navigate to other pages
        private void Button_GameIntro_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GameIntroPage();
        }

        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {         
            this.NavigationService.Content = new CreateUser_Page();
        }

        private void PlayWithoutUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GamePage(isPlayAgain);
        }
        #endregion
    }
}
