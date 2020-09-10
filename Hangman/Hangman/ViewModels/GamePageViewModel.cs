using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
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
        public ICommand GameStartCommand { get; set; }
        public ICommand StopWatchHideCommand { get; set; }
        public string Timer { get; set; }
        public bool IsStopWatchView { get; private set; }

        private DispatcherTimer dispatcherTimer;
        private Stopwatch stopWatch;


        public IWord Word { get; set; }

        //public ICommand ShowHintCommand { get; set; }


            // Denna metod för att visa och dölja ledtråden?
        //public void ShowHint()
        //{
        //    Hint = Word.Hint;
        //}


        public GamePageViewModel()
        {
            GameStartCommand = new RelayCommand(StartGame);
            IsStopWatchView = true;
            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);
            MakeStopWatch();

            //ShowHintCommand = new RelayCommand(ShowHint);
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
            Word = Word_Repository.GetRandomWord();
            StartStopWatch();
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
