using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using Hangman.Views;
using Hangman.Views.Highscore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscorePageViewModel : BaseViewModel
    {
        public TopGamesUC TopGames { get; set; }
        public TopGamesCurrentPlayerUC TopGamesCurrentPlayer { get; set; }
        public TopPlayersUC TopPlayers { get; set; }

        public HighscorePageViewModel()
        {
            var playerId = ActivePlayer?.Id;
            if (playerId != null)
            {
                TopGamesCurrentPlayer = new TopGamesCurrentPlayerUC(playerId);
            }
            TopGames = new TopGamesUC();
            TopPlayers = new TopPlayersUC();
        }
    }
}
