using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.ViewModels
{
    public class LetterKeyboardViewModel : BaseViewModel
    {
        private static readonly char[] _lettersABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();
        private static readonly char[] _lettersQWERTY = "QWERTYUIOPÅASDFGHJKLÖÄZXCVBNM".ToCharArray();

        public StackPanel Keyboard { get; set; } = new StackPanel();

        private readonly Dictionary<char, Button> _keyboardButtons = new Dictionary<char, Button>();

        public LetterKeyboardViewModel()
        {
        }

        public void CreateLetterButtons(ICommand keyCommand, bool isQwerty = true)
        {
            var letters = isQwerty ? _lettersQWERTY : _lettersABC;
            var row1 = letters.Take(11);
            var row2 = letters.Skip(11).Take(11);

            var grids = new List<Grid>();
            for (int i = 0; i < 3; i++)
            {
                grids.Add(new Grid());
            }

            grids[1].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
            grids[2].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            foreach (var c in letters)
            {
                var b = new Button
                {
                    Content = c,
                    Command = keyCommand, //new RelayParameterizedCommand(p => LetterClick((char)p)),
                    CommandParameter = c,
                    Style = Application.Current.FindResource("KeyButton") as Style
                };

                _keyboardButtons.Add(c, b);

                if (row1.Contains(c))
                {
                    grids[0].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    Grid.SetColumn(b, grids[0].ColumnDefinitions.Count - 1);
                    grids[0].Children.Add(b);
                }
                else if (row2.Contains(c))
                {
                    grids[1].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    Grid.SetColumn(b, grids[1].ColumnDefinitions.Count - 1);
                    grids[1].Children.Add(b);
                }
                else
                {
                    grids[2].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    Grid.SetColumn(b, grids[2].ColumnDefinitions.Count - 1);
                    grids[2].Children.Add(b);
                }
            }

            grids[0].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
            grids[2].ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.5, GridUnitType.Star) });
            foreach (var g in grids)
            {
                Keyboard.Children.Add(g);
            }
        }

        public void MarkLetterCorrect(char letter)
        {
            var b = _keyboardButtons[letter];

            b.Opacity = 0.3;
            b.Foreground = Brushes.Green;
            b.FontWeight = FontWeights.Bold;
        }

        public void MarkLetterIncorrect(char letter)
        {
            var b = _keyboardButtons[letter];

            b.Opacity = 0.3;
            b.Foreground = Brushes.Red;
            b.FontWeight = FontWeights.Bold;
        }
    }
}
