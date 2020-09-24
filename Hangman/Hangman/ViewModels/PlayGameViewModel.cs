using Hangman.Models;
using Hangman.Moduls.InterfacesForDatabase;
using Hangman.Repositories;
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
    public class PlayGameViewModel : BaseViewModel
    {
        private static readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();
        private static readonly int _incorrectGuessLimit = 10;
        private static readonly string _letterPlaceHolder = "_";


        public WrapPanel Keyboard { get; set; } = new WrapPanel();
        public Grid WordDisplay { get; set; }
        public string GameStateImage { get; set; } = @"..\..\..\Assets\Images\hänggubbe0.png";

        private Word currentWord;
        public char[] CurrentWordArray { get => currentWord?.Name.ToUpper().ToCharArray(); }
        public string CurrentWordHint { get => currentWord?.Hint; }


        private readonly Dictionary<char, Button> _keyboardButtons = new Dictionary<char, Button>();
        private readonly Dictionary<char, List<TextBlock>> _wordTextBlocks = new Dictionary<char, List<TextBlock>>();


        private bool isGameInProgress;
        private int numberOfIncorrectGuesses;
        private DateTime currentGameStartTime;

        private readonly IWordRepository wordRepository;
        private readonly IGameRepository gameRepository;
        public PlayGameViewModel()
        {
            wordRepository = new WordRepository();
            gameRepository = new GameRepository();
            CreateLetterButtons();

            currentWord = wordRepository.GetRandomWord();
            CreateWordTextBlocks();
        }

        private void CreateLetterButtons()
        {
            foreach (var c in _letters)
            {
                var b = new Button
                {
                    Content = c,
                    Command = new RelayParameterizedCommand(p => LetterClick((char)p)),
                    CommandParameter = c,
                    Style = Application.Current.FindResource("KeyButton") as Style
                };

                _keyboardButtons.Add(c, b);
                Keyboard.Children.Add(b);
            }
        }

        private void CreateWordTextBlocks()
        {
            var grid = new Grid();

            int currentGridColumn = 0;
            foreach (var c in CurrentWordArray)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                var tb = new TextBlock();
                tb.Text = _letterPlaceHolder;
                tb.FontSize = 58;
                Grid.SetColumn(tb, currentGridColumn);

                if (_wordTextBlocks.TryGetValue(c, out List<TextBlock> items))
                {
                    items.Add(tb);
                }
                else
                {
                    _wordTextBlocks.Add(c, new List<TextBlock> { tb });
                }

                grid.Children.Add(tb);
                currentGridColumn++;
            }

            WordDisplay = grid;
        }

        private void LetterClick(char letter)
        {
            if (!isGameInProgress)
            {
                StartGame();
            }

            if (CurrentWordArray.Contains(letter))
            {
                foreach (var tb in _wordTextBlocks[letter])
                {
                    tb.Text = letter.ToString();
                }

                MarkLetterCorrect(_keyboardButtons[letter]);

                if (_wordTextBlocks.All(o => o.Value.All(u => u.Text != _letterPlaceHolder)))
                {
                    GameOver(true);
                }
            }
            else
            {
                MarkLetterIncorrect(_keyboardButtons[letter]);
                numberOfIncorrectGuesses++;

                if (numberOfIncorrectGuesses >= _incorrectGuessLimit)
                {
                    GameOver(false);
                }
            }
        }

        private void StartGame()
        {
            isGameInProgress = true;
            numberOfIncorrectGuesses = 0;
            currentGameStartTime = DateTime.Now;
            //TODO
        }

        private void MarkLetterCorrect(Button b)
        {
            b.Opacity = 0.3;
            b.Foreground = Brushes.Green;
            b.FontWeight = FontWeights.Bold;
        }

        private void MarkLetterIncorrect(Button b)
        {
            b.Opacity = 0.3;
            b.Foreground = Brushes.Red;
            b.FontWeight = FontWeights.Bold;
        }

        private void GameOver(bool isWin)
        {
            //TODO

            if (ActivePlayer != null)
            {
                var game = new Game
                {
                    NumberOfIncorrectTries = numberOfIncorrectGuesses,
                    StartTime = currentGameStartTime,
                    EndTime = DateTime.Now,
                    PlayerId = ActivePlayer.Id,
                    WordId = currentWord.Id
                };

                gameRepository.AddGame(game);
            }
        }
    }
}
