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

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void GameStartIntro_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GameIntroPage();
        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new HighScore_Page();
        }
    }
}
