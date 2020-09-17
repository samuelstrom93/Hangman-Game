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

        public IPlayer Player;
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

        #region MnuMethods
        

        private void mnuLogOut(object sender, RoutedEventArgs e)
        {
            //leder tillbaka användaren till LoginSkärmen
            this.NavigationService.Content = new LoginPage();
        }

        private void mnuDeleteUser(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new UserSettingsPage();
        }

        #endregion

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

            if (gamePageViewModel.isWon)
            {
                this.NavigationService.Content = new GameSuccess_Page(gamePageViewModel.GetGameScore(), gamePageViewModel.GetWord());
            }
            if (gamePageViewModel.isLost)
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
            
        }

    }
}
