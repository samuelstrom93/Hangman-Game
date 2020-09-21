using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace Hangman.Moduls
{
    class StopWatchEngine : BaseViewModel
    {
        public ICommand StopWatchHideCommand { get; set; }

        public string Timer { get; set; }   // Binding: GamePage.xml
        public bool IsStopWatchView { get; set; } // Binding: GamePage.xml

        private DispatcherTimer dispatcherTimer;
        private Stopwatch stopWatch;

        public StopWatchEngine()
        {
            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);
        }

        public void MakeStopWatch()
        {
            Timer = "00:00:00";
            dispatcherTimer = new DispatcherTimer();
            stopWatch = new Stopwatch();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        public void HideOrViewStopWatch()
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

        public void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                Timer = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }

        public void StartStopWatch()
        {
            stopWatch.Start();
            dispatcherTimer.Start();
        }

        public void StopStopWatch()    //Använd när det här spelet slutar
        {
            stopWatch.Stop();
            dispatcherTimer.Stop();
        }

        public void ResetStopWatch()   //Använd när ett nytt spel startar
        {
            stopWatch.Reset();
            Timer = "00:00:00";
        }
    }
}
