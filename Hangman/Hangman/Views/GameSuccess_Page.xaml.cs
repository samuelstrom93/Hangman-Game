using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
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
    /// Interaction logic for GameSuccess_Page.xaml
    /// </summary>
    public partial class GameSuccess_Page : Page
    {
        private GameSuccessPageViewModel gameSuccessPageViewModel;
        public GameSuccess_Page(Game game, Word word)
        {
            InitializeComponent();
            gameSuccessPageViewModel = new GameSuccessPageViewModel();
            DataContext = gameSuccessPageViewModel;

            gameSuccessPageViewModel.SetGame(game);
            gameSuccessPageViewModel.SetNumberOfCorrectTries();
            gameSuccessPageViewModel.GetWord(word);
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
