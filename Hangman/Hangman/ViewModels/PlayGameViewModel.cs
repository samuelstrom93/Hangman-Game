using Hangman.Models;
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

        private GameEndViewModel gameEndVM;
        public GameEndUC GameEndOverlay { get; set; }

        public Grid WordDisplay { get; set; }

        public string GameStateImage { get; set; } = @"..\..\..\Assets\Images\hänggubbe0.png";
        public Visibility HintVisibility { get; set; } = Visibility.Hidden;
        public ICommand ShowHintCommand { get; set; }

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

            ShowHintCommand = new RelayCommand(ShowHint);
            stopWatchVM = new StopWatchUCViewModel();
            StopWatch = new StopWatchUC(stopWatchVM);

            keyboardVM = new LetterKeyboardViewModel();
            Keyboard = new LetterKeyboardUC(keyboardVM);
            keyboardVM.CreateLetterButtons(new RelayParameterizedCommand(p => LetterClick((char)p)));
        }

        private void ShowHint()
        {
            if (HintVisibility == Visibility.Hidden)
            {
                HintVisibility = Visibility.Visible;
            }
            else
            {
                HintVisibility = Visibility.Hidden;
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

                keyboardVM.MarkLetterCorrect(letter);

                if (_wordTextBlocks.All(o => o.Value.All(u => u.Text != _letterPlaceHolder)))
                {
                    GameOver(true);
                }
            }
            else
            {
                keyboardVM.MarkLetterIncorrect(letter);
                numberOfIncorrectGuesses++;

                GameStateImage = $@"..\..\..\Assets\Images\hänggubbe{numberOfIncorrectGuesses}.png";

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
            stopWatchVM.StartStopWatch();
            //TODO
        }

        private void GameOver(bool isWin)
        {
            //TODO
            var game = new Game
            {
                NumberOfIncorrectTries = numberOfIncorrectGuesses,
                StartTime = currentGameStartTime,
                EndTime = DateTime.Now,
                PlayerId = ActivePlayer?.Id ?? 0,
                WordId = currentWord.Id,
                IsWon = isWin
            };

            stopWatchVM.StopStopWatch();

            if (ActivePlayer != null)
            {
                game.Id = gameRepository.AddGame(game);
            }

            GameEndOverlay = new GameEndUC(new GameEndViewModel(game));
        }
    }
}
