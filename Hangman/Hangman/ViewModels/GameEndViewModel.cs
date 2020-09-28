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
        public PlayerStatsUC PlayerStats { get; set; }

        private readonly IHighscoreRepository _highscoreRepository;

        public GameEndViewModel(Game game)
        {
            _highscoreRepository = new HighscoreRepository();

            Title = game.IsWon ? _winTitle : _lossTitle;
            TitleColor = game.IsWon ? "Green" : "Red";

            IncorrectGuesses = $"Antal felgissningar: {game.NumberOfIncorrectTries}";
            TotalTime = "Tid: " + (game.EndTime - game.StartTime).ToString(@"mm\:ss\.fff");

            if (game.Id != 0)
            {
                PlayerStats = new PlayerStatsUC();

                if (game.IsWon)
                {
                    var gameRanking = _highscoreRepository.GetRankOnHighScore(game.Id);
                    GameRankDisplay = $"Du kom på plats: {gameRanking}";
                }
            }
        }

    }
}
