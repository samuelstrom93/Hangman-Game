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
        


        #endregion

        #region CommandsForKeys
        public ICommand SelectedKeyCommandA { get; set; }
        public ICommand SelectedKeyCommandB { get; set; }
        public ICommand SelectedKeyCommandC { get; set; }
        public ICommand SelectedKeyCommandD { get; set; }
        public ICommand SelectedKeyCommandE { get; set; }
        public ICommand SelectedKeyCommandF { get; set; }
        public ICommand SelectedKeyCommandG { get; set; }
        public ICommand SelectedKeyCommandH { get; set; }
        public ICommand SelectedKeyCommandI { get; set; }
        public ICommand SelectedKeyCommandJ { get; set; }
        public ICommand SelectedKeyCommandK { get; set; }
        public ICommand SelectedKeyCommandL { get; set; }
        public ICommand SelectedKeyCommandM { get; set; }
        public ICommand SelectedKeyCommandN { get; set; }
        public ICommand SelectedKeyCommandO { get; set; }
        public ICommand SelectedKeyCommandP { get; set; }
        public ICommand SelectedKeyCommandQ { get; set; }
        public ICommand SelectedKeyCommandR { get; set; }
        public ICommand SelectedKeyCommandS { get; set; }
        public ICommand SelectedKeyCommandT { get; set; }
        public ICommand SelectedKeyCommandU { get; set; }
        public ICommand SelectedKeyCommandV { get; set; }
        public ICommand SelectedKeyCommandW { get; set; }
        public ICommand SelectedKeyCommandX { get; set; }
        public ICommand SelectedKeyCommandY { get; set; }
        public ICommand SelectedKeyCommandZ { get; set; }
        public ICommand SelectedKeyCommandOO { get; set; }
        public ICommand SelectedKeyCommandAA { get; set; }
        public ICommand SelectedKeyCommandAE { get; set; }
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
        private string selectedKey;
        private string upperWord;


        public GamePageViewModel()
        {
            MakeDemoPlayer(); //TA BORT SENARE

            GameStartCommand = new RelayCommand(StartGame);
            MakeCommandsForKeys();
            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);

            IsStopWatchView = true;
            MakeStopWatch();
        }

        
        private void JudgeGame()
        {

            if (numberOfTriesMAX == 0)
            {
                EndGame();
            }
            
        }

        private void EndGame()
        {
            game.EndTime = DateTime.Now;
            StopStopWatch();
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
            upperWord = word.Name.ToUpper();
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

        #region MethodsForKeys
        private void MakeCommandsForKeys()
        {
            SelectedKeyCommandA = new RelayCommand(SelectKeyA);
            SelectedKeyCommandB = new RelayCommand(SelectKeyB);
            SelectedKeyCommandC = new RelayCommand(SelectKeyC);
            SelectedKeyCommandD = new RelayCommand(SelectKeyD);
            SelectedKeyCommandE = new RelayCommand(SelectKeyE);
            SelectedKeyCommandF = new RelayCommand(SelectKeyF);
            SelectedKeyCommandG = new RelayCommand(SelectKeyG);
            SelectedKeyCommandH = new RelayCommand(SelectKeyH);
            SelectedKeyCommandI = new RelayCommand(SelectKeyI);
            SelectedKeyCommandJ = new RelayCommand(SelectKeyJ);
            SelectedKeyCommandK = new RelayCommand(SelectKeyK);
            SelectedKeyCommandL = new RelayCommand(SelectKeyL);
            SelectedKeyCommandM = new RelayCommand(SelectKeyM);
            SelectedKeyCommandN = new RelayCommand(SelectKeyN);
            SelectedKeyCommandO = new RelayCommand(SelectKeyO);
            SelectedKeyCommandP = new RelayCommand(SelectKeyP);
            SelectedKeyCommandQ = new RelayCommand(SelectKeyQ);
            SelectedKeyCommandR = new RelayCommand(SelectKeyR);
            SelectedKeyCommandS = new RelayCommand(SelecKeyS);
            SelectedKeyCommandT = new RelayCommand(SelectKeyT);
            SelectedKeyCommandU = new RelayCommand(SelectKeyU);
            SelectedKeyCommandV = new RelayCommand(SelectKeyV);
            SelectedKeyCommandW = new RelayCommand(SelectKeyW);
            SelectedKeyCommandX = new RelayCommand(SelectKeyX);
            SelectedKeyCommandY = new RelayCommand(SelectKeyY);
            SelectedKeyCommandZ = new RelayCommand(SelectKeyZ);
            SelectedKeyCommandAA = new RelayCommand(SelectKeyAA);
            SelectedKeyCommandAE = new RelayCommand(SelectKeyAE);
            SelectedKeyCommandOO = new RelayCommand(SelectKeyOO);
        }

        private void SelectKeyA()
        {
            selectedKey ="A";
        }

        private void SelectKeyB()
        {
            selectedKey = "B";
        }

        private void SelectKeyC()
        {
            selectedKey = "C";
        }

        private void SelectKeyD()
        {
            selectedKey = "D";
        }

        private void SelectKeyE()
        {
            selectedKey = "E";
        }

        private void SelectKeyF()
        {
            selectedKey = "F";
        }

        private void SelectKeyG()
        {
            selectedKey = "G";
        }

        private void SelectKeyH()
        {
            selectedKey = "H";
        }

        private void SelectKeyI()
        {
            selectedKey = "I";
        }

        private void SelectKeyJ()
        {
            selectedKey = "J";
        }

        private void SelectKeyK()
        {
            selectedKey = "K";
        }

        private void SelectKeyL()
        {
            selectedKey = "L";
        }

        private void SelectKeyM()
        {
            selectedKey = "M";
        }

        private void SelectKeyN()
        {
            selectedKey = "N";
        }

        private void SelectKeyO()
        {
            selectedKey = "O";
        }

        private void SelectKeyQ()
        {
            selectedKey = "Q";
        }

        private void SelectKeyP()
        {
            selectedKey = "P";
        }

        private void SelectKeyR()
        {
            selectedKey = "R";
        }

        private void SelecKeyS()
        {
            selectedKey = "S";
        }

        private void SelectKeyT()
        {
            selectedKey = "T";
        }

        private void SelectKeyU()
        {
            selectedKey = "U";
        }

        private void SelectKeyV()
        {
            selectedKey = "V";
        }

        private void SelectKeyW()
        {
            selectedKey = "W";
        }

        private void SelectKeyX()
        {
            selectedKey = "X";
        }

        private void SelectKeyY()
        {
            selectedKey = "Y";
        }

        private void SelectKeyZ()
        {
            selectedKey = "Z";
        }

        private void SelectKeyAA()
        {
            selectedKey = "AA";
        }

        private void SelectKeyAE()
        {
            selectedKey = "AE";
        }

        private void SelectKeyOO()
        {
            selectedKey = "OO";
        }


        #endregion
    }
}
