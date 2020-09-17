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
using Hangman;
using Hangman.ViewModels;
using Hangman.Repositories;
using Hangman.Models;
using Hangman.GameLogics;

namespace Hangman.Views

{
    /// <summary>
    /// Interaction logic for UpdateUserPage.xaml
    /// </summary>
    public partial class UpdateUserPage : Page
    {
        private UpdateUserViewModel model;
        public IPlayer Player;

        public UpdateUserPage(IPlayer player)
        {
            InitializeComponent();
            Player = player;
            model = new UpdateUserViewModel(player);
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxInsertName.Text;

            //Uppdatera Player Engine
            model.UpdateUser(PlayerEngine.ActivePlayer, name);

            //Byter namn på menyn också (Buggsäkra om användaren skriver in "")
            model.PlayerName = PlayerEngine.ActivePlayer.Name;

            txtBoxInsertName.Clear();
            DataContext = model;
        }
    }
}
