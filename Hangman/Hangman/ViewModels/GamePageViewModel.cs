using Hangman.Models;
using Hangman.ViewModels.Base;
using static Hangman.Repositories.Player_Repository;
using static Hangman.Repositories.Word_Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace Hangman.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        #region Commands

        public ICommand GameStartCommand { get; set; }
        public ICommand StopWatchHideCommand { get; set; }
        public ICommand GameJudgeCommand { get; set; }


        #endregion

        #region StopWatch

        public string Timer { get; set; }
        public bool IsStopWatchView { get; private set; }

        private DispatcherTimer dispatcherTimer;
        private Stopwatch stopWatch;

        #endregion

        #region PropertiesForGameStart

        private IPlayer playerTEST { get; set; }    //TA BORT SENARE
        private IGame game { get; set; }
        private Word word { get; set; }

        #endregion

        private int numberOfTriesMAX;   // 0 =GAME OVER
        private int numberOfTies;
        private int numberOfIncorrectTries;


        public GamePageViewModel()
        {
            MakeDemoPlayer(); //TA BORT SENARE

            GameStartCommand = new RelayCommand(StartGame);
            IsStopWatchView = true;
            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);
            MakeStopWatch();
        }

        private void MakeDemoPlayer()
        {
            string testPlayerName = "TestMan";
            //CreatePlayer(testPlayerName);
            playerTEST = GetPlayer(testPlayerName);
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

        private void MakeStopWatch()
        {
            Timer = "00:00:00";
            dispatcherTimer = new DispatcherTimer();
            stopWatch = new Stopwatch();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
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

        private void StartGame()
        {
            StartStopWatch();
            MakeWord();
            MakeGame();
        }

        private void MakeWord()
        {
            word = GetRandomWord();
        }

        private void MakeGame()
        {
            game = new Game
            {
                IsWon = false,
                NumberOfIncorrectTries = 0,
                NumberOfTries = 0,
                StartTime = DateTime.Now,
                PlayerId = playerTEST.Id,
                WordId = word.Id

            };

            numberOfTriesMAX = 11;
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
    }
}
