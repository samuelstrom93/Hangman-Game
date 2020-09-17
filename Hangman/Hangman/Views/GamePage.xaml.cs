using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        #region public field
        public IPlayer Player;

        #endregion
        #region private field
        private GamePageViewModel gamePageViewModel;
        #endregion

        public GamePage()
        {
            InitializeComponent();

            gamePageViewModel = new GamePageViewModel();
            DataContext = gamePageViewModel;

            GameStart.Content = new GameStartPage();
        }

        public GamePage(IPlayer player)
        {

            InitializeComponent();
            Player = player;

            gamePageViewModel = new GamePageViewModel(player);
            DataContext = gamePageViewModel;

            GameStart.Content = new GameStartPage();
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            if (gamePageViewModel.IsGameStart)
            {
                string selectedKey = ((Button)sender).Content.ToString();

                gamePageViewModel.TakeSelectedKey(selectedKey);
                gamePageViewModel.JudgeGame();

                ChangeBtnStyle((Button)sender);
                //((Button)sender).IsEnabled = false;
            }

            if (gamePageViewModel.IsGameEnd)
            {
                this.NavigationService.Content = new GameEnd_Page(gamePageViewModel.GetGameScore(), gamePageViewModel.GetWord());
            }

        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GameIntroPage();
        }

        private void GameIntroPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new GameIntroPage();
        }

        private void GuessDirectlyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (gamePageViewModel.IsGameStart)
            {
                gamePageViewModel.TakeGuessingAnswer(guessingWordText.Text);
                gamePageViewModel.GuessDirectly();
                gamePageViewModel.SwitchGameStatus();
            }
            if (gamePageViewModel.IsGameEnd)
            {
                this.NavigationService.Content = new GameEnd_Page(gamePageViewModel.GetGameScore(), gamePageViewModel.GetWord());
            }

        }

        private void ChangeBtnStyle(Button sender)
        {
            if (gamePageViewModel.IsGuessCorrect)
            {
                //((Button)sender).Background = Brushes.Green;
                sender.Opacity = 0.5;
                sender.BorderThickness = new Thickness(0, 0, 0, 0);
                sender.BorderBrush = null;
                sender.Foreground = Brushes.Green;
            }
            else
            {
                //((Button)sender).Background = Brushes.Red;
                sender.Opacity = 0.5;
                sender.BorderThickness = new Thickness(0, 0, 0, 0);
                sender.BorderBrush = null;
                sender.Foreground = Brushes.Red;

            }
            sender.IsEnabled = false;

        }


    }
}
