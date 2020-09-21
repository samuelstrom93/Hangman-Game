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

        public GamePage(bool isPlayAgain)
        {
            InitializeComponent();

            gamePageViewModel = new GamePageViewModel();
            DataContext = gamePageViewModel;

            ViewGameStartPageAsOverray(isPlayAgain);
        }

        public GamePage(IPlayer player, bool isPlayAgain)
        {

            InitializeComponent();
            Player = player;

            gamePageViewModel = new GamePageViewModel(player);
            DataContext = gamePageViewModel;

            ViewGameStartPageAsOverray(isPlayAgain);

        }


        #region Methods: LetterBtn
        /* private void Letter_Click(object sender, RoutedEventArgs e)
         {
             JudgeGameFromLetterClick(((Button)sender));
             ViewGameEndPage();
         }


         private void JudgeGameFromLetterClick(Button sender)
         {
             if (gamePageViewModel.IsGameStart)
             {
                 string selectedKey = sender.Content.ToString();

                 gamePageViewModel.TakeSelectedKey(selectedKey);
                 gamePageViewModel.JudgeGame();

                 ChangeBtnStyle(sender);
             }
         }

         private void ChangeBtnStyle(Button sender)
         {
             if (gamePageViewModel.IsGuessCorrect)
             {
                 sender.Opacity = 0.3;
                 sender.Foreground = Brushes.Green;
                 sender.FontWeight = FontWeights.Bold;
             }
             else
             {
                 sender.Opacity = 0.3;
                 sender.Foreground = Brushes.Red;
                 sender.FontWeight = FontWeights.Bold;

             }
             sender.IsEnabled = false;

         }*/

        #endregion

        #region Methods: GuessDirectlyBtn
        private void GuessDirectlyBtn_Click(object sender, RoutedEventArgs e)
        {
            /*JudgeGameFromGuessDirectly();
            ViewGameEndPage();*/
        }

        /*private void JudgeGameFromGuessDirectly()
        {
            if (gamePageViewModel.GameEngine.IsGameStart)
            {
                gamePageViewModel.TakeGuessingAnswer(guessingWordText.Text);
                gamePageViewModel.GuessDirectly();
                gamePageViewModel.GameEngine.SwitchGameStatus();
            }
        }*/
        #endregion

        #region Methods: View/Jump other pages

        private void ViewGameStartPageAsOverray(bool isPlayAgain)
        {
            if (isPlayAgain == false)
            {
                Overray.Content = new GameStartPage();
            }
        }

        private void ViewGameEndPage()
        {
            if (gamePageViewModel.GameEngine.IsGameEnd)
            {
                //this.NavigationService.Content = new GameEnd_Page(gamePageViewModel.GetGameScore(), gamePageViewModel.GetWord());
                Overray.Content = new GameEnd_Page(gamePageViewModel.GameEngine.GetGame(), gamePageViewModel.GameEngine.GetWord());
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
        #endregion




    }
}
