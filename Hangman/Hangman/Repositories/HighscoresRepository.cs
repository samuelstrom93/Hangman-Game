using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hangman.Repositories
{
    public static class HighscoresRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        /// <summary>
        /// Hämtar topplistan för spel
        /// </summary>
        /// <param name="playerId">SpelarId om man vill hämta topplista för en specifik spelare</param>
        /// <param name="numHighscores">Hur lång toppplista man vill ha</param>
        /// <returns>Lista med toppspel</returns>
        public static IEnumerable<HighscoreGame> GetTopGames(int? playerId = null, int numHighscores = 10)
        {
            return null;
        }

        /// <summary>
        /// Hämta mest frekventa spelarna
        /// </summary>
        /// <returns>Dictionary med spelarnamn som nyckel och antal spelade spel som värde.</returns>
        public static IDictionary<string, int> GetTopDiligent(int numPlayers = 10)
        {
            return null;
        }
    }
}
