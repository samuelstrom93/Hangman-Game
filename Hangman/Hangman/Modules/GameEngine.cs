using Hangman.Models;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using static Hangman.Repositories.PlayerRepository;
using static Hangman.Repositories.WordRepository;
using static Hangman.Repositories.GameRepository;
using Hangman.Moduls;
using Hangman.Repositories;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace Hangman.Modules
{
    public class GameEngine : BaseViewModel
    {
        public StopWatchUCViewModel StopWatchEngine { get; set; }

        public bool IsGuessCorrect { get; set; }
        public bool IsGameStart { get; set; }
        public bool IsGameEnd { get; set; }
        public bool IsWon;

        public IPlayer IPlayer { get; set; }
        public IWord IWord { get; set; }

        public bool IsStartBtnClickable { get; set; }   //Binding i GamePage.xml
        public string AnswerForPlayer { get; set; }     //Binding i GamePage.xml

        private WordRepository wordRepository;





        public GameEngine()
        {
            wordRepository = new WordRepository();
            IsGameStart = false;
            IsStartBtnClickable = true;

            MakeWord();
            MakeWordArray();
        }

        #region GetAndSet
        public void SetStopWatch(StopWatchUCViewModel stopWatchUCViewModel) 
        {
            StopWatchEngine = stopWatchUCViewModel;
        }

        public Game GetGame()
        {
            return game;
        }

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
        #endregion

        #region GameStart

        private Player Player { get; set; }
        public void SetPlayer(IPlayer iplayer)
        {
            Player = new Player()
            {
                Id = iplayer.Id,
                Name = iplayer.Name
            };
        }

        public void SetPlayerWithNoID()
        {
            Player = new Player()
            {
                Id = 0
            };
        }

        public void StartGame()
        {
            IsStartBtnClickable = false;
            IsGameStart = true;
            IsGameEnd = false;

            MakeGame();

        }

        private string upperWord;
        private int[] wordCheckerArray; // int[] =0 →gissat FEL, int[] =1 →gissat RÄTT
        private char[] answerForPlayerArray { get; set; }

        private void MakeWord()
        {
            IWord = wordRepository.GetRandomWord();
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

        private Game game { get; set; }
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

        private int gameStage;
        public BitmapImage ImageForGameStage { get; set; }  //Binding i GamePage.xml

        public BitmapImage heartImage { get; set; }
        public BitmapImage heartImage2 { get; set; }


        internal void ShowGameStage()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/hänggubbe{gameStage}.png";
            string currentPath = Environment.CurrentDirectory;

            ImageForGameStage = new BitmapImage(new Uri(System.IO.Path.Combine(currentPath, imageAdress)));

            BreakHearts(); //Lägga hjärtan här
        } 

        private int numberOfLives;   // 0 =GAME OVER
        private int numberOfTries;
        private int numberOfIncorrectTries;

        internal void RefreshGame()
        {
            SetPlayerWithNoID();
            numberOfLives = 10;
            numberOfTries = 0;
            numberOfIncorrectTries = 0;
            NumberOfIncorrectTries_text = numberOfIncorrectTries.ToString();    //Binding GamePage.xml

            gameStage = 0;
            IsWon = false;
        }



        #endregion

        #region GameJudge
        private string selectedKey { get; set; }
        public void JudgeGame(string selectedkey)
        {
            selectedKey = selectedkey;
            CompareWordAndSelectedKey();
            WorkCounters();
            ConvertShownWord();
            LinkAnswerForPlayer();
            SwitchGameStatus();
        }

        private void CompareWordAndSelectedKey()
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

        private void WorkCounters()
        {
            if (upperWord.Contains(selectedKey))    //Gissade rätt
            {
                numberOfTries++;
                IsGuessCorrect = true;
            }
            else //Gissade fel
            {
                numberOfTries++;
                numberOfLives = numberOfLives - 1;
                numberOfIncorrectTries++;
                IsGuessCorrect = false;
                gameStage++;
                ShowGameStage();
            }
        }

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


        private void LinkAnswerForPlayer()
        {
            AnswerForPlayer = "";
            for (int i = 0; i < answerForPlayerArray.Length; i++)
            {
                AnswerForPlayer += $"{answerForPlayerArray[i]}  ";
            }
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

        public void GuessDirectly(string playersGuessingAnswer)
        {
            if (playersGuessingAnswer == upperWord) //Spelaren vann
            {
                numberOfTries++;
                IsWon = true;
                EndGame();
            }

            else //Gissade fel
            {
                numberOfTries++;
                numberOfLives = numberOfLives - 1;
                numberOfIncorrectTries++;
                IsGuessCorrect = false;
                gameStage++;
                ShowGameStage();
            }
        }

        #endregion

        #region GameEnd
        private void EndGame()
        {
            game.EndTime = DateTime.Now;
            StopWatchEngine.StopStopWatch();
            SetGameScore();
            IsGameStart = false;
            IsGameEnd = true;
        }

        private void SetGameScore()
        {
            game.NumberOfIncorrectTries = numberOfIncorrectTries;
            game.NumberOfTries = numberOfTries;
            game.IsWon = IsWon;
        }
        #endregion


        #region LIVES

        //IZA FÖR EXTRALIV
        public string extraliv1 { get; set; } = "10";
        public string extraliv2 { get; set; } = "10";
        public string extraliv3 { get; set; } = "10";
        public string extraliv4 { get; set; } = "10";
        public string extraliv5 { get; set; } = "10";
        public string extraliv6 { get; set; } = "10";
        public string extraliv7 { get; set; } = "10";
        public string extraliv8 { get; set; } = "10";
        public string extraliv9 { get; set; } = "10";
        public string extraliv10 { get; set; } = "10";


        public void BreakHearts()
        {

            switch (gameStage)
            {
                case 1:
                    extraliv1 = "0:0:1";
                    break;
                case 2:
                    extraliv2 = "0:0:1";
                    break;
                case 3:
                    extraliv3 = "0:0:1";
                    break;
                case 4:
                    extraliv4 = "0:0:1";
                    break;
                case 5:
                    extraliv5 = "0:0:1";
                    break;
                case 6:
                    extraliv6 = "0:0:1";
                    break;
                case 7:
                    extraliv7 = "0:0:1";
                    break;
                case 8:
                    extraliv8 = "0:0:1";
                    break;
                case 9:
                    extraliv9 = "0:0:1";
                    break;
                case 10:
                    extraliv10 = "0:0:1";
                    break;
            }
        }
        #endregion
    }
}
