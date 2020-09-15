﻿using Hangman.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hangman.Repositories
{
    public static class HighscoreRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

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
                + "\ninner join player on player.id = player_id"
                + "\ninner join word on word.id = word_id"
                + "\nwhere is_won is true"
                + $"{(playerId.HasValue ? $" and player_id=@playerid" : string.Empty)}"
                + "\norder by number_of_incorrect_tries, game_time"
                + $"\nlimit {numHighscores}";

            using (var conn = new NpgsqlConnection(_connectionString))
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
                            return result;
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
        public static IDictionary<string, long> GetTopDiligentPlayers(int numPlayers = 10)
        {
            var result = new Dictionary<string, long>();
            var queryString = "select player.name as player_name, (select count(game)) as count from game"
                + "\ninner join player on player.id = player_id"
                + "\nwhere is_won is true"
                + "\ngroup by player_name"
                + "\norder by count desc"
                + $"\nlimit {numPlayers}";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(queryString, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return result;
                        }

                        while (reader.Read())
                        {
                            result.Add((string)reader["player_name"], (long)reader["count"]);
                        }
                    }
                }
            }

            return result;
        }

        // Hämtar tiden utifrån valt gameId
        public static double GetGameTime(double gameId)
        {
            string stmt = $"SELECT EXTRACT(EPOCH FROM (end_time - start_time)) FROM game WHERE id = {gameId}";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                double gameTime;
                using (var command = new NpgsqlCommand())
                {
                    conn.Open();
                    command.Connection = conn;
                    command.CommandText = stmt;
                    gameTime = (double)command.ExecuteScalar();
                    return gameTime;
                }
            }
        }

        public static IEnumerable<HighscoreGame> GetGamesFromTime()
        {

            // SQL-query för att få fram id och tiddifferens
//            SELECT EXTRACT(EPOCH FROM (end_time -start_time)), game.id
//FROM game
//ORDER BY date_part ASC;


            string stmt = $"SELECT EXTRACT(EPOCH FROM (end_time -start_time)), game.id FROM game ORDER BY date_part ASC";
            var gameDiffTime = new List<HighscoreGame>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(_connectionString, conn))
                {
                    
                    using (var reader = command.ExecuteReader())
                    {
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
                            gameDiffTime.Add(row);
                        }
                    }
                }
            }
            return gameDiffTime;
        }
    }
}
