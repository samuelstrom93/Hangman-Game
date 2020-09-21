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

namespace Hangman.Views.UCsForGamePage
{
    /// <summary>
    /// Keyboard.xaml の相互作用ロジック
    /// </summary>
    public partial class KeyboardUC : UserControl
    {
        public KeyboardViewModel KeyboardViewModel { get; set; }
        public KeyboardUC()
        {
            InitializeComponent();
            KeyboardViewModel = new KeyboardViewModel();
            DataContext = KeyboardViewModel;

        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            JudgeGameFromLetterClick(((Button)sender));
            ViewGameEndPage();
        }

        private void JudgeGameFromLetterClick(Button sender)
        {
            if (KeyboardViewModel.GameEngine.IsGameStart)
            {
                string selectedKey = sender.Content.ToString();
                KeyboardViewModel.GameEngine.JudgeGame(selectedKey);

                ChangeBtnStyle(sender);
            }
        }

        private void ChangeBtnStyle(Button sender)
        {
            if (KeyboardViewModel.GameEngine.IsGuessCorrect)
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
        }

        private void ViewGameEndPage()
        {
            if (KeyboardViewModel.GameEngine.IsGameEnd)
            {
                KeyboardViewModel.GameEndPage = new GameEndPage(KeyboardViewModel.GameEngine.GetGame(), KeyboardViewModel.GameEngine.GetWord());
            }
        }
    }
}
