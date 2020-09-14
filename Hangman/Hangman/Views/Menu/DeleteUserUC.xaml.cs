using Hangman.GameLogics;
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
using Hangman.ViewModels;
using Hangman.Models;
using System.ComponentModel.Design;

namespace Hangman.Views.Menu
{
    /// <summary>
    /// Interaction logic for DeleteUserUC.xaml
    /// </summary>
    public partial class DeleteUserUC : UserControl
    {

        private DeleteUserViewModel model;
        public IPlayer Player;

        public DeleteUserUC()
        {
            InitializeComponent();
            model = new DeleteUserViewModel(PlayerEngine.ActivePlayer);
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxInsertUser.Text;

            if (PlayerEngine.IsNameUsed(name) && name == PlayerEngine.ActivePlayer.Name)
            {
                model.DeleteUser(PlayerEngine.ActivePlayer.Name);
                MessageBox.Show("Din användare är nu raderad - du loggas nu ut");
              //  this.NavigationService.Content = new LoginPage();
            }

            else if (name != PlayerEngine.ActivePlayer.Name)
            {
                MessageBox.Show("Du har skrivit in fel användarnamn");
                txtBoxInsertUser.Clear();
            }
            DataContext = model;
        }
    }
}
