using Hangman.Models;
using Hangman.Moduls;
using Hangman.Moduls.InterfacesForDatabase;
using Hangman.Repositories;
using Hangman.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class PlayGameViewModel : BaseViewModel
    {
        private static readonly int _incorrectGuessLimit = 10;
        private static readonly string _letterPlaceHolder = "_";

        public bool GameEndVisibility { get; set; } = false;
        public GameEndViewModel GameEndViewModel { get; set; }
        public LetterKeyboardViewModel KeyboardViewModel { get; set; }
        public StopWatchUCViewModel StopWatchViewModel { get; set; }

        public ObservableCollection<HeartDisplayViewModel> LivesDisplay { get; set; }
        public ObservableCollection<char> WordDisplay { get; set; }

        public string GuessBox { get; set; }
        public string GameStateImage { get; set; } = @"..\..\..\Assets\Images\hänggubbe0.png";
        public bool HintVisibility { get; set; } = false;
        public bool HintButtonIsEnabled => !HintVisibility;
        public ICommand ShowHintCommand { get; set; }
        public ICommand GuessDirectlyCommand { get; set; }

        private Word currentWord;
        public char[] CurrentWordArray { get => currentWord?.Name.ToUpper().ToCharArray(); }
        public string CurrentWordHint { get => currentWord?.Hint; }
        public int CurrentWordLength { get => currentWord?.Name.Length ?? 0; }

        private bool isGameInProgress;
        private int numberOfIncorrectGuesses;
        private DateTime currentGameStartTime;

        private readonly IWordRepository wordRepository;
        private readonly IGameRepository gameRepository;

        public PlayGameViewModel()
        {
            wordRepository = new WordRepository();
            gameRepository = new GameRepository();
            KeyboardViewModel = new LetterKeyboardViewModel(new RelayParameterizedCommand(p => LetterClick((char)p)));
            LivesDisplay = new ObservableCollection<HeartDisplayViewModel>();
            WordDisplay = new ObservableCollection<char>();

            currentWord = wordRepository.GetRandomWord();
            CreateWordTextBlocks();
            CreateLifeDisplay();

            ShowHintCommand = new RelayCommand(ShowHint);
            GuessDirectlyCommand = new RelayCommand(GuessDirectly);
            StopWatchViewModel = new StopWatchUCViewModel();
        }

        #region UIComponents
        private void CreateLifeDisplay()
        {
            LivesDisplay.Clear();
            for (int i = 0; i < 10; i++)
            {
                LivesDisplay.Add(new HeartDisplayViewModel());
            }
        }

        private void CreateWordTextBlocks()
        {
            foreach (var _ in CurrentWordArray)
            {
                WordDisplay.Add(char.Parse(_letterPlaceHolder));
            }
        }
        #endregion

        #region GameLogic
        private void GuessDirectly()
        {
            if (string.IsNullOrEmpty(GuessBox))
            {
                return;
            }

            if (!isGameInProgress)
            {
                StartGame();
            }

            if (GuessBox.Equals(currentWord.Name, StringComparison.OrdinalIgnoreCase))
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

            HintVisibility = true;
            IncorrectGuess();
        }

        private void LetterClick(char letter)
        {
            if (!isGameInProgress)
            {
                StartGame();
            }

            if (CurrentWordArray.Contains(letter))
            {
                foreach (var indexOfLetter in CurrentWordArray.Select((c, i) => c == letter ? i : -1).Where(i => i != -1))
                {
                    WordDisplay[indexOfLetter] = letter;
                }

                KeyboardViewModel.MarkLetterUsed(letter, true);

                if (!WordDisplay.Any(c => c == char.Parse(_letterPlaceHolder)))
                {
                    GameOver(true);
                }
            }
            else
            {
                KeyboardViewModel.MarkLetterUsed(letter, false);
                IncorrectGuess();
            }
        }

        private void IncorrectGuess()
        {
            LivesDisplay[numberOfIncorrectGuesses].AnimationDuration = TimeSpan.FromMilliseconds(400).ToString(@"hh\:mm\:ss\.ff");

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
            StopWatchViewModel.StartStopWatch();
            currentGameStartTime = DateTime.Now;
        }

        private void GameOver(bool isWin)
        {
            StopWatchViewModel.StopStopWatch();

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

            GameEndViewModel = new GameEndViewModel(game, currentWord.Name);
            GameEndVisibility = true;
        }
        #endregion
    }
}
