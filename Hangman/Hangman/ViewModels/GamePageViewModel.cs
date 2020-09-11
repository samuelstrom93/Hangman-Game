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
using System.Windows.Controls;
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

        private IPlayer playerTEST { get; set; }    //TA BORT SENARE
        private Game Game { get; set; }
        public bool IsGameStart { get; set; }

        #endregion

        #region ForGameScore
        public int numberOfLife;   // 0 =GAME OVER
        public int numberOfTies;
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


        public GamePageViewModel()
        {
            MakeDemoPlayer(); //TA BORT SENARE

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

        #region Methods:GameStart-End
        private void StartGame()
        {
            MakeWord();
            MakeGame();
            RefreshGame();
            StartStopWatch();
            IsHintShown = false;
        }

        private void MakeWord()
        {
            IWord = GetRandomWord();
            upperWord = IWord.Name.ToUpper();
        }

        private void MakeGame()
        {
            Game = new Game
            {
                IsWon = false,
                NumberOfIncorrectTries = 0,
                NumberOfTries = 0,
                StartTime = DateTime.Now,
                PlayerId = playerTEST.Id,
                WordId = IWord.Id

            };
        }

        private void RefreshGame()
        {
            numberOfLife = 10;
            numberOfTies = 0;
            numberOfIncorrectTries = 0;
            numberOfCorrectTries = 0;
            numberOfCorrectTries_text = numberOfCorrectTries.ToString();
            numberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
            isWon = false;
            IsGameStart = true;
        }

        public void JudgeGame()
        {
            if (upperWord.Contains(selectedKey))    //Gissade rätt
            {
                numberOfTies++;
                numberOfCorrectTries++;
                numberOfCorrectTries_text = numberOfCorrectTries.ToString();
                IsGuessCorrect = true;
            }
            else //Gissade fel
            {
                numberOfTies++;
                numberOfLife = numberOfLife-1;
                numberOfIncorrectTries++;
                numberOfIncorrectTries_text = numberOfIncorrectTries.ToString();
                IsGuessCorrect = false;
            }

            if (numberOfCorrectTries == 6)  //Spelaren vann
            {
                isWon = true;
                EndGame();
            }

            if (numberOfLife == 0)  //Game over
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
            Game.NumberOfTries = numberOfTies;
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
