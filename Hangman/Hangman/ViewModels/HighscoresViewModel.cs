using Hangman.GameLogics;
using Hangman.Models;
using Hangman.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hangman.ViewModels
{
    public class HighscoresViewModel
    {
        //public List<HighscoreGame> TopHighscores { get; set; }
        //public List<HighscoreGame> TopCurrentPlayerHighscores { get; set; }

        //public Dictionary<string, long> TopDiligentPlayers { get; set; }




        public ObservableCollection<HighscoreGame> TopCurrentPlayerHighscores { get; set; }

        public ObservableCollection<HighscoreGame> Leaderboard { get; set; }

        public IDictionary<string, long> TopDiligentPlayers { get; set; }

        public string Title { get; set; }

        public HighscoresViewModel()
        {
            // Skicka player via konstruktor istället?
            var player = PlayerEngine.ActivePlayer;

            // null som parameter betyder att den global leaderboard hämtas
            Leaderboard = HighscoreRepository.GetLeaderboard(null);
            
            TopCurrentPlayerHighscores = HighscoreRepository.GetLeaderboard(player.Id);
            TopDiligentPlayers = HighscoreRepository.GetTopDiligentPlayers();
        }
    }
}
