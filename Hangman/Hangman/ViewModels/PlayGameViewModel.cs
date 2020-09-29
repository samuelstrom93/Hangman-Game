﻿using Hangman.Models;
using Hangman.Moduls;
using Hangman.Moduls.InterfacesForDatabase;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using Hangman.Views;
using Hangman.Views.PlayGame;
using Hangman.Views.UCsForGamePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace Hangman.ViewModels
{
    public class PlayGameViewModel : BaseViewModel
    {
        private static readonly int _incorrectGuessLimit = 10;
        private static readonly string _letterPlaceHolder = "_";

        private LetterKeyboardViewModel keyboardVM;
        public LetterKeyboardUC Keyboard { get; set; }

        private StopWatchUCViewModel stopWatchVM;
        public StopWatchUC StopWatch { get; set; }

        public GameEndUC GameEndOverlay { get; set; }

        public Grid LifeDisplay { get; set; }
        public Grid WordDisplay { get; set; }
        public string GuessBox { get; set; }
        public string GameStateImage { get; set; } = @"..\..\..\Assets\Images\hänggubbe0.png";
        public bool IsQWERTYChecked { get; set; } = true;
        public Visibility HintVisibility { get; set; } = Visibility.Hidden;
        public ICommand ShowHintCommand { get; set; }
        public ICommand GuessDirectlyCommand { get; set; }
        public ICommand KeyboardLayoutCommand { get; set; }

        private Word currentWord;
        public char[] CurrentWordArray { get => currentWord?.Name.ToUpper().ToCharArray(); }
        public string CurrentWordHint { get => currentWord?.Hint; }

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

            currentWord = wordRepository.GetRandomWord();
            CreateWordTextBlocks();
            CreateLifeDisplay();

            ShowHintCommand = new RelayCommand(ShowHint);
            GuessDirectlyCommand = new RelayCommand(GuessDirectly);
            KeyboardLayoutCommand = new RelayCommand(ChangeKeyboardLayout);
            stopWatchVM = new StopWatchUCViewModel();
            StopWatch = new StopWatchUC(stopWatchVM);

            keyboardVM = new LetterKeyboardViewModel();
            Keyboard = new LetterKeyboardUC(keyboardVM);
            keyboardVM.CreateLetterButtons(new RelayParameterizedCommand(p => LetterClick((char)p)));
        }

        private void ChangeKeyboardLayout()
        {
            keyboardVM.CreateLetterButtons(new RelayParameterizedCommand(p => LetterClick((char)p)), IsQWERTYChecked);
        }

        #region UIComponents
        private void CreateLifeDisplay()
        {
            Grid grid = new Grid();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("../../../Views/Gifs/HeartBreak0.gif", UriKind.Relative);
            image.EndInit();

            for (int i = 0; i < 10; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                var img = new Image();
                ImageBehavior.SetAnimatedSource(img, image);
                ImageBehavior.SetAnimationDuration(img, new Duration(TimeSpan.FromMilliseconds(300)));
                ImageBehavior.SetAutoStart(img, false);
                Grid.SetColumn(img, i);

                grid.Children.Add(img);
            }

            LifeDisplay = grid;
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
        #endregion

        #region GameLogic
        private void GuessDirectly()
        {
            if (!isGameInProgress)
            {
                StartGame();
            }

            if (GuessBox != null && GuessBox.Equals(currentWord.Name, StringComparison.OrdinalIgnoreCase))
            {
                GameOver(true);
            }
            else
            {
                IncorrectGuess();
            }

            GuessBox = null;
        }

        private void ShowHint()
        {
            if (!isGameInProgress)
            {
                StartGame();
            }

            if (HintVisibility == Visibility.Hidden)
            {
                HintVisibility = Visibility.Visible;
                //TODO: Spelarens liv minskas
            }
            else
            {
                HintVisibility = Visibility.Hidden;
            }
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

                keyboardVM.MarkLetterCorrect(letter);

                if (_wordTextBlocks.All(o => o.Value.All(u => u.Text != _letterPlaceHolder)))
                {
                    GameOver(true);
                }
            }
            else
            {
                keyboardVM.MarkLetterIncorrect(letter);
                IncorrectGuess();
            }
        }

        private void IncorrectGuess()
        {
            var controller = ImageBehavior.GetAnimationController((Image)LifeDisplay.Children[numberOfIncorrectGuesses]);
            controller.Play();

            numberOfIncorrectGuesses++;
            GameStateImage = $@"..\..\..\Assets\Images\hänggubbe{numberOfIncorrectGuesses}.png";

            if (numberOfIncorrectGuesses >= _incorrectGuessLimit)
            {
                GameOver(false);
            }
        }

        private void StartGame()
        {
            isGameInProgress = true;
            numberOfIncorrectGuesses = 0;
            currentGameStartTime = DateTime.Now;
            stopWatchVM.StartStopWatch();
        }

        private void GameOver(bool isWin)
        {
            stopWatchVM.StopStopWatch();

            var game = new Game
            {
                NumberOfIncorrectTries = numberOfIncorrectGuesses,
                StartTime = currentGameStartTime,
                EndTime = DateTime.Now,
                PlayerId = ActivePlayer?.Id ?? 0,
                WordId = currentWord.Id,
                IsWon = isWin
            };

            if (ActivePlayer != null)
            {
                game.Id = gameRepository.AddGame(game);
            }

            GameEndOverlay = new GameEndUC(new GameEndViewModel(game, currentWord.Name));
        }
        #endregion
    }
}