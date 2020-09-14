using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.Player_Repository;
using static Hangman.Repositories.Word_Repository;
using static Hangman.Repositories.Game_Repository;
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

namespace Hangman.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        #region Commands

        public ICommand GameStartCommand { get; set; }
        public ICommand StopWatchHideCommand { get; set; }

        #endregion

        #region StopWatch

        public string Timer { get; set; }
        public bool IsStopWatchView { get; private set; }

        private DispatcherTimer dispatcherTimer;
        private Stopwatch stopWatch;

        #endregion

        #region PropertiesForGameStart
        public string PlayerName { get; set; }
        public IPlayer IPlayer { get; set; }
        private IPlayer playerTEST { get; set; }    //TA BORT SENARE
        private Game Game { get; set; }
        public bool IsGameStart { get; set; }

        #endregion

        #region ForGameScore
        public int numberOfLives;   // 0 =GAME OVER
        public int numberOfTries;
        private int numberOfIncorrectTries;
        private int numberOfCorrectTries;
        public bool isWon;

        public string numberOfCorrectTries_text { get; set; }
        public string numberOfIncorrectTries_text { get; set; }

        #endregion

        #region ForJudgeGame
        private string selectedKey;
        private string upperWord;

        public bool IsGuessCorrect { get; set; }

        #endregion


        #region Hint
        public IWord IWord { get; set; }
        public ICommand ShowHintCommand { get; set; }
        public bool IsHintShown { get; set; }

        public void ShowHint()
        {
            if (IsHintShown == true)
            {
                IsHintShown = false;
            }
            else
            {
                IsHintShown = true;
            }
            
        }

        #endregion Hint

        public GamePageViewModel(IPlayer player)
        {
            PlayerName = PlayerEngine.ActivePlayer.Name;
            IPlayer = player;
            //MakeDemoPlayer(); //TA BORT SENARE

            GameStartCommand = new RelayCommand(StartGame);
            //BtnStyleChangeCommand = new RelayCommand(JudgeBtnStyle);
            //MakeSelectedBtn();

            //MakeCommandsForKeys();

            MakeStopWatch();


            ShowHintCommand = new RelayCommand(ShowHint);


            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);
            IsStopWatchView = true;

            IsGameStart = false;

        }

        

        private void MakeDemoPlayer() //TESTKOD. TA BORT SENARE
        {
            string testPlayerName = "TestMan";
            //CreatePlayer(testPlayerName);
            playerTEST = GetPlayer(testPlayerName);
        }

        #region Methods: GameStart

        private void StartGame()
        {
            MakeWord();
            MakeGame();
            MakeWordArray();
            RefreshGame();
            StartStopWatch();
            IsHintShown = false;
        }

        private void MakeWord()
        {
            IWord = GetRandomWord();
            upperWord = IWord.Name.ToUpper();
            ShowingWordArray = new char[upperWord.Length];
            wordCheckerArray = new int[upperWord.Length];
            MakeFirstAnswerForPlayer();
            LinkAnswerForPlayer();
        }

        private void MakeFirstAnswerForPlayer()
        {
            for (int i = 0; i < ShowingWordArray.Length; i++) 
            {
                ShowingWordArray[i] = '_';
            } 
            
        }

        private void MakeGame()
        {
            Game = new Game
            {
                IsWon = false,
                NumberOfIncorrectTries = 0,
                NumberOfTries = 0,
                StartTime = DateTime.Now,
                PlayerId = IPlayer.Id,
                WordId = IWord.Id
            };
        }

        private char[] wordArray;
        private void MakeWordArray()
        {
            wordArray = new char[upperWord.Length];
            for (int i = 0; i < upperWord.Length; i++)
            {
                char oneOfWord = upperWord[i];
                wordArray[i] = oneOfWord;
            }

        }

        private void RefreshGame()
        {
            numberOfLives = 10;
            numberOfTries = 0;
            numberOfIncorrectTries = 0;
            numberOfCorrectTries = 0;
            numberOfCorrectTries_text = numberOfCorrectTries.ToString();
            numberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
            isWon = false;
            IsGameStart = true;
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

        private int[] wordCheckerArray; // int[] =0 →FEL, int[] =1 →RÄTT
        public void CompareWordAndSelectedKey()
        {
            for (int i = 0; i < wordArray.Length; i++)
            {
                char oneOfWord = wordArray[i];
                char selectedKeyChar = selectedKey[0];

                if (oneOfWord == selectedKeyChar)   //Gissade rätt
                {
                    wordCheckerArray[i] = 1;
                }

            }
        }

        public char[] ShowingWordArray { get; set; }
        private char[] ConvertShownWord()
        {
            for (int i = 0; i < wordArray.Length; i++)
            {

                if (wordCheckerArray[i] == 1)
                {
                    ShowingWordArray[i] = wordArray[i];
                }
                if (wordCheckerArray[i] == 0)
                {
                    ShowingWordArray[i] = '_';
                }
            }
            return ShowingWordArray;
        }

        public string AnswerForPlayer { get; set; } //Binding i GamePage.xml
        public void LinkAnswerForPlayer()
        {
            AnswerForPlayer ="";
            for(int i = 0; i<ShowingWordArray.Length; i++)
            {
                AnswerForPlayer += $"{ShowingWordArray[i]}  ";
            }

        }

        public void WorkCounters()
        {

            if (upperWord.Contains(selectedKey))    //Gissade rätt
            {
                numberOfTries++;
                numberOfCorrectTries++;
                numberOfCorrectTries_text = numberOfCorrectTries.ToString();
                IsGuessCorrect = true;
            }
            else //Gissade fel
            {
                numberOfTries++;
                numberOfLives = numberOfLives - 1;
                numberOfIncorrectTries++;
                numberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
                IsGuessCorrect = false;
                ProceedGameStage();
            }
        }
        private int gameStage;
        private void ProceedGameStage()
        {
            throw new NotImplementedException();
        }

        private void SwitchGameStatus()
        {
            string answer = new string(ShowingWordArray);

            if (answer == upperWord) //Spelaren vann
            {
                isWon = true;
                EndGame();
            }

            if (numberOfLives == 0)  //Game over
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            Game.EndTime = DateTime.Now;
            StopStopWatch();
            SaveGameScore();
            IsGameStart = false;
        }

        private void SaveGameScore()
        {
            Game.NumberOfIncorrectTries = numberOfIncorrectTries;
            Game.NumberOfTries = numberOfTries;
            Game.IsWon = isWon;

            AddGame(Game);
        }

        #endregion

        #region Methods:StopWatch
        private void MakeStopWatch()
        {
            Timer = "00:00:00";
            dispatcherTimer = new DispatcherTimer();
            stopWatch = new Stopwatch();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void HideOrViewStopWatch()
        {
            if (IsStopWatchView == true)
            {
                IsStopWatchView = false;
            }
            else
            {
                IsStopWatchView = true;
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                Timer = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }




        private void StartStopWatch()
        {
            stopWatch.Start();
            dispatcherTimer.Start();
        }

        private void StopStopWatch()    //Använd när det här spelet slutar
        {
            stopWatch.Stop();
            dispatcherTimer.Stop();
        }

        private void ResetStopWatch()   //Använd när ett nytt spel startar
        {
            stopWatch.Reset();
            Timer = "00:00:00";
        }
        #endregion

        #region MethodForSelectedBtn
        public void TakeSelectedKey(string selectedkey)
        {
            selectedKey = selectedkey;
        }
      
        #endregion
    }
}
