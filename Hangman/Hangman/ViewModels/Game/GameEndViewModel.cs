using Hangman.Database;
using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Hangman.ViewModels
{
    public class GameEndViewModel : BaseViewModel
    {
        private static readonly string _winTitle = "Vinst";
        private static readonly string _lossTitle = "Förlust";

        public string Title { get; set; }
        public string TitleColor { get; set; }
        public string GameRankDisplay { get; set; }
        public string IncorrectGuesses { get; set; }
        public string TotalTime { get; set; }
        public string Word { get; set; }
        public bool PlayerStatsBoolToVis { get; set; } = false;

        private readonly IHighscoreRepository _highscoreRepository;

        public GameEndViewModel(Game game, string word)
        {
            _highscoreRepository = new HighscoreRepository();

            Title = game.IsWon ? _winTitle : _lossTitle;
            TitleColor = game.IsWon ? "Green" : "Red";

            IncorrectGuesses = $"Antal felgissningar: {game.NumberOfIncorrectTries}";
            TotalTime = "Tid: " + (game.EndTime - game.StartTime).ToString(@"mm\:ss\.ff");
            Word = word.ToUpper();

            if (game.Id != 0)
            {
                PlayerStatsBoolToVis = true;

                if (game.IsWon)
                {
                    var gameRanking = _highscoreRepository.GetRankOnHighScore(game.Id);
                    GameRankDisplay = $"Du kom på plats: {gameRanking}";
                }
            }
        }

    }
}
