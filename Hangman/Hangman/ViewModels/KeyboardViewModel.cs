using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.ViewModels
{
    public class KeyboardViewModel : BaseViewModel
    {
        public string SelectedKey { get; set; }
        public ICommand LetterClickCommand { get; set; }
        public bool IsGameStart { get; set; }
        public bool IsGuessCorrect { get; set; }
        public Button Button { get; set; }

        public KeyboardViewModel(string selectedKey)
        {
            SelectedKey = selectedKey;
            Button = new Button();

            LetterClickCommand = new RelayCommand(ChangeBtnStyle);
        }

        public void SetIsGuessCorrect(bool isGuessCorrect)
        {
            IsGuessCorrect = isGuessCorrect;
        }
        public void SetIsGameStart(bool isGameStart)
        {
            IsGameStart = IsGameStart;
        }

        private void ChangeBtnStyle()
        {
            if (IsGuessCorrect)
            {
                Button.Opacity = 0.3;
                Button.Foreground = Brushes.Green;
                Button.FontWeight = FontWeights.Bold;
            }
            else
            {
                Button.Opacity = 0.3;
                Button.Foreground = Brushes.Red;
                Button.FontWeight = FontWeights.Bold;

            }
            Button.IsEnabled = false;

        }
    }
}
