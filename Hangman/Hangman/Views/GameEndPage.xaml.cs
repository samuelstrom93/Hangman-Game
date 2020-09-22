using Hangman.Models;
using Hangman.ViewModels;
using static Hangman.Repositories.PlayerRepository;
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
    /// Gameover_Page.xaml の相互作用ロジック
    /// </summary>
    public partial class GameEndPage : Page
    {
        private GameEndPageViewModel gameEndPageViewModel;
        private bool isPlayAgain = true;

        public GameEndPage(Game game, Word word)
        {
            InitializeComponent();
            gameEndPageViewModel = new GameEndPageViewModel(game, word);
            DataContext = gameEndPageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(gameEndPageViewModel.GameEndEngine.GetGame().PlayerId != 0)    // Behåller inloggning
            {
                this.NavigationService.Content = new GamePage(GetPlayerFromID(gameEndPageViewModel.GameEndEngine.GetGame().PlayerId), isPlayAgain);
            }

            else
            {
                this.NavigationService.Content = new GamePage(isPlayAgain);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new LoginPage();
        }
    }
}
