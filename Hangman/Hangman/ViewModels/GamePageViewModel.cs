using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.PlayerRepository;
using static Hangman.Repositories.WordRepository;
using static Hangman.Repositories.GameRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using Hangman.Views;
using Hangman.GameLogics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.IO;
using System.Drawing;
using System.Reflection;
using Hangman.Moduls;
using Hangman.Views.UCsForGamePage;

namespace Hangman.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        #region Commands

        public ICommand GameStartCommand { get; set; }

        #endregion

        #region StopWatch
        public StopWatchUC StopWatchUC { get; set; }
        public StopWatchUCViewModel StopWatchEngine { get; set; }

        private void MakeStopWatchUC()
        {
            StopWatchEngine = new StopWatchUCViewModel();
            StopWatchUC = new StopWatchUC(StopWatchEngine);
        }

        #endregion

        #region Hint
        public HintUC HintUC { get; set; }
        public IWord IWord { get; set; }
        #endregion Hint

        #region PropertiesForGameStart

        public string PlayerName { get; set; }  // = PlayerEngine.ActivePlayer.Name
        public IPlayer IPlayer { get; set; }
        private Game game { get; set; }

        public bool IsGameStart { get; set; }
        public bool IsGameEnd { get; set; }

        public bool IsStartBtnClickable { get; set; }

        #endregion

        #region ForGameScore

        private int numberOfLives;   // 0 =GAME OVER
        private int numberOfTries;
        private int numberOfIncorrectTries;
        private int numberOfCorrectTries;

        public bool IsWon;
        public string NumberOfCorrectTries_text { get; set; }
        public string NumberOfIncorrectTries_text { get; set; }

        #endregion

        #region ForJudgeGame
        private string selectedKey;
        private string upperWord;

        public bool IsGuessCorrect { get; set; }

        #endregion

        public GamePageViewModel()  // UTAN inloggning
        {
            PlayerName = "Spela utan användare";
            SetPlayerWithoutLoggIn();

            RefreshGame();
            ViewGameStage();

            SetCommands();

            MakeStopWatchUC();
            HintUC = new HintUC();

            IsGameStart = false;
            IsStartBtnClickable = true;

        }


        public GamePageViewModel(IPlayer player)    // MED inloggning
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
            SetPlayer(player);

            RefreshGame();
            ViewGameStage();

            SetCommands();

            MakeStopWatchUC();

            IsGameStart = false;
            IsStartBtnClickable = true;

        }

        #region GetMethods: Word + Game

        public Word GetWord()
        {
            Word word = new Word
            {
                Id = IWord.Id,
                Name = IWord.Name,
                Hint = IWord.Hint
            };
            return word;
        }

        public Game GetGameScore()
        {
            return game;
        }

        #endregion

        #region SetMethods

        private Player Player { get; set; }
        private void SetPlayer(IPlayer iplayer)
        {
            Player = new Player() 
            {
                Id = iplayer.Id,
                Name = iplayer.Name
            };
        }
        private void SetPlayerWithoutLoggIn()
        {
            Player = new Player()
            {
                Id = 0
            };
        }

        private void SetCommands()
        {
            GameStartCommand = new RelayCommand(StartGame);
            //ShowHintCommand = new RelayCommand(ShowHint);
            
        }
        #endregion

        #region Methods: GameStart

        private void StartGame()
        {
            IsStartBtnClickable = false;
            MakeWord();

            MakeGame();

            MakeWordArray();

            StopWatchEngine.StartStopWatch();
            HintUC.SetDataContext(IWord.Hint);


            IsGameStart = true;
            IsGameEnd = false;
        }

        private void MakeWord()
        {
            IWord = GetRandomWord();
            upperWord = IWord.Name.ToUpper();

            answerForPlayerArray = new char[upperWord.Length];  
            MakeFirstAnswerForPlayer();

            wordCheckerArray = new int[upperWord.Length];

            LinkAnswerForPlayer();
        }

        private void MakeFirstAnswerForPlayer()
        {
            for (int i = 0; i < answerForPlayerArray.Length; i++) 
            {
                answerForPlayerArray[i] = '_';
            } 
        }

        private void MakeGame()
        {
            game = new Game
            {
                IsWon = false,
                NumberOfIncorrectTries = 0,
                NumberOfTries = 0,
                StartTime = DateTime.Now,
                PlayerId = Player.Id,
                WordId = IWord.Id
            };
        }

        private char[] upperWordArray;
        private void MakeWordArray()
        {
            upperWordArray = new char[upperWord.Length];
            for (int i = 0; i < upperWord.Length; i++)
            {
                char oneOfUpperWord = upperWord[i];
                upperWordArray[i] = oneOfUpperWord;
            }
        }

        private void RefreshGame()
        {
            numberOfLives = 10;
            numberOfTries = 0;
            numberOfIncorrectTries = 0;
            numberOfCorrectTries = 0;

            NumberOfCorrectTries_text = numberOfCorrectTries.ToString();    //Binding GamePage.xml
            NumberOfIncorrectTries_text = numberOfIncorrectTries.ToString();    //Binding GamePage.xml

            gameStage = 0;
            IsWon = false;
        }

        #endregion

        #region Methods: JudgeGame-End

        public void JudgeGame()
        {
            CompareWordAndSelectedKey();
            WorkCounters();
            ConvertShownWord();
            LinkAnswerForPlayer();
            SwitchGameStatus();
        }

        private int[] wordCheckerArray; // int[] =0 →gissat FEL, int[] =1 →gissat RÄTT
        public void CompareWordAndSelectedKey()
        {
            for (int i = 0; i < upperWordArray.Length; i++)
            {
                char oneOfWord = upperWordArray[i];
                char selectedKeyChar = selectedKey[0];

                if (oneOfWord == selectedKeyChar)   //Spelaren gissade rätt
                {
                    wordCheckerArray[i] = 1;
                }
            }
        }

        private char[] answerForPlayerArray { get; set; }
        private char[] ConvertShownWord()
        {
            for (int i = 0; i < upperWordArray.Length; i++)
            {

                if (wordCheckerArray[i] == 1)
                {
                    answerForPlayerArray[i] = upperWordArray[i];
                }
                if (wordCheckerArray[i] == 0)
                {
                    answerForPlayerArray[i] = '_';
                }
            }
            return answerForPlayerArray;
        }

        public string AnswerForPlayer { get; set; } //Binding i GamePage.xml
        public void LinkAnswerForPlayer()
        {
            AnswerForPlayer ="";
            for(int i = 0; i<answerForPlayerArray.Length; i++)
            {
                AnswerForPlayer += $"{answerForPlayerArray[i]}  ";
            }

        }

        public void WorkCounters()
        {

            if (upperWord.Contains(selectedKey))    //Gissade rätt
            {
                numberOfTries++;
                numberOfCorrectTries++;
                NumberOfCorrectTries_text = numberOfCorrectTries.ToString();
                IsGuessCorrect = true;
            }
            else //Gissade fel
            {
                numberOfTries++;
                numberOfLives = numberOfLives - 1;
                numberOfIncorrectTries++;
                NumberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
                IsGuessCorrect = false;
                gameStage++;
                ViewGameStage();
            }
        }

        private int gameStage;
        public BitmapImage ImageForGameStage { get; set; }
        private void ViewGameStage()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/hänggubbe{gameStage}.png";

            string currentPath = Environment.CurrentDirectory;
            ImageForGameStage = new BitmapImage( new Uri( System.IO.Path.Combine(currentPath, imageAdress)));
        }

        public void SwitchGameStatus()
        {
            string answer = new string(answerForPlayerArray);

            if (answer == upperWord) //Spelaren vann
            {
                IsWon = true;
                EndGame();
            }

            if (numberOfLives == 0)  //Game over
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            game.EndTime = DateTime.Now;
            StopWatchEngine.StopStopWatch();
            SaveGameScore();
            IsGameStart = false;
            IsGameEnd = true;
        }

        private void SaveGameScore()
        {
            game.NumberOfIncorrectTries = numberOfIncorrectTries;
            game.NumberOfTries = numberOfTries;
            game.IsWon = IsWon;
        }

        #endregion

        #region SelectedBtn + GuessDirectlyBtn
        KeyboardUC Keyboard { get; set; }
        KeyboardViewModel KeyboardViewModel { get; set; }
        public void TakeSelectedKey(string selectedkey)
        {
            selectedKey = selectedkey;
        }

        private string playersGuessingAnswer;
        public void TakeGuessingAnswer(string guessingAnswer)
        {
            playersGuessingAnswer = guessingAnswer.ToUpper();
        }

        public void GuessDirectly()
        {
            if (playersGuessingAnswer == upperWord) //Spelaren vann
            {
                numberOfTries++;
                numberOfCorrectTries++;
                NumberOfCorrectTries_text = numberOfCorrectTries.ToString();

                IsWon = true;
                EndGame();
            }
            else //Gissade fel
            {
                numberOfTries++;
                numberOfLives = numberOfLives - 1;
                numberOfIncorrectTries++;
                NumberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
                IsGuessCorrect = false;
                gameStage++;
                ViewGameStage();
            }
        }
        #endregion
    }
}
