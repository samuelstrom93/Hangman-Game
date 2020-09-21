using Hangman.Models;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using static Hangman.Repositories.PlayerRepository;
using static Hangman.Repositories.WordRepository;
using static Hangman.Repositories.GameRepository;

namespace Hangman.Moduls
{
    public class GameEngine : BaseViewModel
    {
        public StopWatchUCViewModel StopWatchEngine { get; set; }

        public bool IsGuessCorrect { get; set; }
        public bool IsGameStart { get; set; }
        public bool IsGameEnd { get; set; }
        public bool IsWon;
        public bool IsStartBtnClickable { get; set; }

        public IPlayer IPlayer { get; set; }
        public IWord IWord { get; set; }

        private Game game { get; set; }

        private string selectedKey{get ; set;}
        private string upperWord;
        private char[] upperWordArray;
        private int[] wordCheckerArray; // int[] =0 →gissat FEL, int[] =1 →gissat RÄTT
        private char[] answerForPlayerArray { get; set; }

        private int numberOfLives;   // 0 =GAME OVER
        private int numberOfTries;
        private int numberOfIncorrectTries;
        private int numberOfCorrectTries;

        public string AnswerForPlayer { get; set; }     //Binding i GamePage.xml
        public string NumberOfCorrectTries_text { get; set; }   //Binding i GamePage.xml
        public string NumberOfIncorrectTries_text { get; set; } //Binding i GamePage.xml

        private int gameStage;
        public BitmapImage ImageForGameStage { get; set; }

        public GameEngine()
        {
            IsGameStart = false;
            IsStartBtnClickable = true;
            
        }

        public void SetStopWatch(StopWatchUCViewModel stopWatchUCViewModel) 
        {
            StopWatchEngine = stopWatchUCViewModel;
        }
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

        public void SetPlayerWithoutLoggIn()
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

            MakeWord();
            MakeGame();
            MakeWordArray();
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
        private void MakeWordArray()
        {
            upperWordArray = new char[upperWord.Length];
            for (int i = 0; i < upperWord.Length; i++)
            {
                char oneOfUpperWord = upperWord[i];
                upperWordArray[i] = oneOfUpperWord;
            }
        }
        internal void ViewGameStage()
        {
            string imageAdress;
            imageAdress = $"../../../Assets/Images/hänggubbe{gameStage}.png";

            string currentPath = Environment.CurrentDirectory;
            ImageForGameStage = new BitmapImage(new Uri(System.IO.Path.Combine(currentPath, imageAdress)));
        }

        internal void RefreshGame()
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

        private void EndGame()
        {
            game.EndTime = DateTime.Now;
            //StopWatchEngine.StopStopWatch();
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

        public Game GetGame()
        {
            return game;
        }

        public void GuessDirectly(string playersGuessingAnswer)
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
    }
}
