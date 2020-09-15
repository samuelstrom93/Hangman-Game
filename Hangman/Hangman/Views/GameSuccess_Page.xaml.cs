using Hangman.GameLogics;
using Hangman.Repositories;
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
    /// Interaction logic for GameSuccess_Page.xaml
    /// </summary>
    public partial class GameSuccess_Page : Page
    {
        private GameSuccess_Page gameSucessPage;
        public GameSuccess_Page()
        {
            InitializeComponent();
            gameSucessPage = new GameSuccess_Page();
            DataContext = gameSucessPage;


            var player = PlayerEngine.ActivePlayer;
            if (player == null)
            {
                //YourTopGames.Content = new TopGamesUC();
                YourTopGames.Content = HighscoreRepository.GetGameTime(66);

            }
            else
            {
                YourTopGames.Content = HighscoreRepository.GetGameTime(66);
                //YourTopGames.Content = HighscoreRepository.GetGamesFromTime();
                //YourTopGames.Content = new TopGamesUC(player.Id);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GamePage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new LoginPage();
        }
    }
}
