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
        private Game game { get; set; }
        private Word word { get; set; }

        #endregion

        #region ForGameScore
        private int numberOfLife;   // 0 =GAME OVER
        private int numberOfTies;
        private int numberOfIncorrectTries;
        private int numberOfcorrectTries;
        private bool isWon;
        #endregion

        #region ForJudgeGame
        private string selectedKey;
        private string upperWord;
        #endregion



        public GamePageViewModel()
        {
            MakeDemoPlayer(); //TA BORT SENARE

            GameStartCommand = new RelayCommand(StartGame);
            MakeCommandsForKeys();

            MakeStopWatch();
            StopWatchHideCommand = new RelayCommand(HideOrViewStopWatch);
            IsStopWatchView = true;
            
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

        }

        private void RefreshGame()
        {
            numberOfLife = 10;
            numberOfTies = 0;
            numberOfIncorrectTries = 0;
            numberOfcorrectTries = 0;
            isWon = false;
        }

        private void JudgeGame()
        {
            if (upperWord.Contains(selectedKey))    //Gissade rätt
            {
                numberOfTies++;
                numberOfcorrectTries++;
            }
            else //Gissade fel
            {
                numberOfTies++;
                numberOfLife = numberOfLife-1;
                numberOfIncorrectTries++;

            }
            if (numberOfcorrectTries == 6)  //Spelaren vann
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
            game.EndTime = DateTime.Now;
            StopStopWatch();
            SaveGameScore();
        }

        private void SaveGameScore()
        {
            game.NumberOfIncorrectTries = numberOfIncorrectTries;
            game.NumberOfTries = numberOfTies;
            game.IsWon = isWon;

            AddGame(game);
        }

        #endregion


        #region MethodsForStopWatch
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
            JudgeGame();
        }

        private void SelectKeyB()
        {
            selectedKey = "B";
            JudgeGame();
        }

        private void SelectKeyC()
        {
            selectedKey = "C";
            JudgeGame();
        }

        private void SelectKeyD()
        {
            selectedKey = "D";
            JudgeGame();
        }

        private void SelectKeyE()
        {
            selectedKey = "E";
            JudgeGame();
        }

        private void SelectKeyF()
        {
            selectedKey = "F";
            JudgeGame();
        }

        private void SelectKeyG()
        {
            selectedKey = "G";
            JudgeGame();
        }

        private void SelectKeyH()
        {
            selectedKey = "H";
            JudgeGame();
        }

        private void SelectKeyI()
        {
            selectedKey = "I";
            JudgeGame();
        }

        private void SelectKeyJ()
        {
            selectedKey = "J";
            JudgeGame();
        }

        private void SelectKeyK()
        {
            selectedKey = "K";
            JudgeGame();
        }

        private void SelectKeyL()
        {
            selectedKey = "L";
            JudgeGame();
        }

        private void SelectKeyM()
        {
            selectedKey = "M";
            JudgeGame();
        }

        private void SelectKeyN()
        {
            selectedKey = "N";
            JudgeGame();
        }

        private void SelectKeyO()
        {
            selectedKey = "O";
            JudgeGame();
        }

        private void SelectKeyQ()
        {
            selectedKey = "Q";
            JudgeGame();
        }

        private void SelectKeyP()
        {
            selectedKey = "P";
            JudgeGame();
        }

        private void SelectKeyR()
        {
            selectedKey = "R";
            JudgeGame();
        }

        private void SelecKeyS()
        {
            selectedKey = "S";
            JudgeGame();
        }

        private void SelectKeyT()
        {
            selectedKey = "T";
            JudgeGame();
        }

        private void SelectKeyU()
        {
            selectedKey = "U";
            JudgeGame();
        }

        private void SelectKeyV()
        {
            selectedKey = "V";
            JudgeGame();
        }

        private void SelectKeyW()
        {
            selectedKey = "W";
            JudgeGame();
        }

        private void SelectKeyX()
        {
            selectedKey = "X";
            JudgeGame();
        }

        private void SelectKeyY()
        {
            selectedKey = "Y";
            JudgeGame();
        }

        private void SelectKeyZ()
        {
            selectedKey = "Z";
            JudgeGame();
        }

        private void SelectKeyAA()
        {
            selectedKey = "AA";
            JudgeGame();
        }

        private void SelectKeyAE()
        {
            selectedKey = "AE";
            JudgeGame();
        }

        private void SelectKeyOO()
        {
            selectedKey = "OO";
            JudgeGame();
        }


        #endregion
    }
}
