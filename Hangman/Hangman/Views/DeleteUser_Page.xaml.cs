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
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for DeleteUserPage.xaml
    /// </summary>
    public partial class DeleteUserPage : Page
    {
        private DeleteUserViewModel model;
        public IPlayer Player;

        public DeleteUserPage(IPlayer player)
        {
            InitializeComponent();
            model = new DeleteUserViewModel(player);
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxInsertUser.Text;

            if (PlayerEngine.IsNameUsed(name) && name == PlayerEngine.ActivePlayer.Name)
            {
                model.DeleteUser(PlayerEngine.ActivePlayer.Name);
                MessageBox.Show("Din användare är nu raderad - du loggas nu ut");
                this.NavigationService.Content = new LoginPage();
            }

            else if (name != PlayerEngine.ActivePlayer.Name)
            {
                MessageBox.Show("Du har skrivit in fel användarnamn");
                txtBoxInsertUser.Clear();
            }
            DataContext = model;

        }

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            this.NavigationService.Content = new LoginPage();
        }

        private void mnuDeleteUser(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new DeleteUserPage(Player);
        }

        private void mnuUpdateUser(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new UpdateUserPage(Player);
        }
    }
}
