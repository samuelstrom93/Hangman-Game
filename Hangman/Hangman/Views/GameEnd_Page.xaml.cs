using Hangman.Models;
using Hangman.ViewModels;
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
    public partial class GameEnd_Page : Page
    {
        private GameEndPageViewModel gameEndPageViewModel;
        public GameEnd_Page(Game game, Word word)
        {
            InitializeComponent();
            gameEndPageViewModel = new GameEndPageViewModel();
            DataContext = gameEndPageViewModel;

            gameEndPageViewModel.SetGame(game);
            gameEndPageViewModel.SetNumberOfCorrectTries();
            gameEndPageViewModel.GetWord(word);
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
