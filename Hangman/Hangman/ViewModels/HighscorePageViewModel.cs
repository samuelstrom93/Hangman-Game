using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Views;
using Hangman.Views.Highscore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscorePageViewModel
    {
        public TopGamesUC TopGames { get; set; }
        public TopGamesCurrentPlayerUC TopGamesCurrentPlayer { get; set; }
        public TopPlayersUC TopPlayers { get; set; }

        public HighscorePageViewModel()
        {
            TopGames = new TopGamesUC();
            TopGamesCurrentPlayer = new TopGamesCurrentPlayerUC();
            TopPlayers = new TopPlayersUC();
        }
    }
}
