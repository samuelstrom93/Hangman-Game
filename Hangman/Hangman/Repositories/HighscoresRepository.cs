using Hangman.Models;
using Npgsql;
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
            var result = new List<HighscoreGame>();
            var queryString = "select number_of_tries, number_of_incorrect_tries, player.name as player_name, word.name as word_name, (select end_time - start_time as game_time) from game"
                + "\nleft join player on player.id = player_id"
                + "\nleft join word on word.id = word_id"
                + "\nwhere is_won is true"
                + $"{(playerId.HasValue ? $"\nwhere player_id=@playerid" : string.Empty)}"
                + "\norder by number_of_incorrect_tries, game_time"
                + $"\nlimit {numHighscores}";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(queryString, conn))
                {
                    if (playerId.HasValue)
                    {
                        command.Parameters.AddWithValue("playerid", playerId.Value);
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        while (reader.Read())
                        {
                            var row = new HighscoreGame
                            {
                                PlayerName = (string)reader["player_name"],
                                Word = (string)reader["word_name"],
                                NumberOfTries = (int)reader["number_of_tries"],
                                NumberOfIncorrectTries = (int)reader["number_of_incorrect_tries"],
                                GameTime = (TimeSpan)reader["game_time"]
                            };

                            result.Add(row);
                        }
                    }
                }
            }

            return result;
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
