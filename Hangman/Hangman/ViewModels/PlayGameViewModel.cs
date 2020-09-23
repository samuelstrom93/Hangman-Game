﻿using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class PlayGameViewModel : BaseViewModel
    {
        public WrapPanel KeyBoard { get; set; } = new WrapPanel();


        private readonly Dictionary<char, Button> KeyBoardButtons;

        private static readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();

        public PlayGameViewModel()
        {
            KeyBoardButtons = new Dictionary<char, Button>();
            CreateButtons();
        }

        private void CreateButtons()
        {
            foreach (var c in _letters)
            {
                var b = new Button
                {
                    Content = c,
                    Command = new RelayParameterizedCommand(p => LetterClick((char)p)),
                    CommandParameter = c,
                    Margin = new Thickness(5),
                    Padding = new Thickness(10),
                    Style = Application.Current.FindResource("StandardButton") as Style
                };

                KeyBoardButtons.Add(c, b);
                KeyBoard.Children.Add(b);
            }
        }

        private void LetterClick(char letter)
        {

        }
    }
}
