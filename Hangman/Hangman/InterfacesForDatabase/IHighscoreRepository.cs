using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hangman.Database
{
    public interface IHighscoreRepository
    {
        IEnumerable<HighscoreGame> GetTopGames(int? playerID, int numHighscore);
        ObservableCollection<HighscoreGame> GetLeaderboard(int? playerID = null, int numHighscores = 20);
        IDictionary<string, long> GetTopDiligentPlayers(int numPlayers);
        int GetRankOnHighScore(int gameID);
    }
}
