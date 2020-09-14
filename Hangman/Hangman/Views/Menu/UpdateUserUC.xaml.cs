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

namespace Hangman.Views.Menu
{
    /// <summary>
    /// Interaction logic for UpdateUserUC.xaml
    /// </summary>
    public partial class UpdateUserUC : UserControl
    {
        private UpdateUserViewModel model;
        public UpdateUserUC()
        {
            InitializeComponent();
            model = new UpdateUserViewModel(PlayerEngine.ActivePlayer);

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
