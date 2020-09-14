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

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GameStartPage.xaml
    /// </summary>
    public partial class GameStartPage : Page
    {
        public GameStartPage()
        {
            InitializeComponent();

            //this.Visibility = Visibility.Hidden;

            var player = PlayerEngine.ActivePlayer;
            if (player == null)
            {
                YourTopGames.Content = new TopGamesUC();
            }
            else
            {
                YourTopGames.Content = new TopGamesUC(player.Id);
            }
        }
    }
}
