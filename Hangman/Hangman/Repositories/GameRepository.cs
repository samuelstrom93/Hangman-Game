using Hangman.Models;
using Hangman.Moduls.InterfacesForDatabase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hangman.Repositories
{
    class GameRepository : IGameRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        #region CREATE
        public int AddGame(Game game)
        {
            string stmt = "INSERT INTO game(is_won, number_of_tries, start_time, end_time, number_of_incorrect_tries, player_id, word_id) values(@is_won ,@number_of_tries, @start_time, @end_time,@number_of_incorrect_tries, @player_id, @word_id) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    using (var command = new NpgsqlCommand(stmt, conn))
                    {
                        try
                        {
                            command.Parameters.AddWithValue("is_won", game.IsWon);
                            command.Parameters.AddWithValue("number_of_tries", game.NumberOfTries);
                            command.Parameters.AddWithValue("start_time", game.StartTime);
                            command.Parameters.AddWithValue("end_time", game.EndTime);
                            command.Parameters.AddWithValue("number_of_incorrect_tries", game.NumberOfIncorrectTries);
                            command.Parameters.AddWithValue("player_id", game.PlayerId);
                            command.Parameters.AddWithValue("word_id", game.WordId);
                            command.Connection = conn;
                            command.CommandText = stmt;
                            command.Prepare();
                            int id = (int)command.ExecuteScalar();
                            trans.Commit();
                            game.Id = id;
                            return id;
                        }
                        catch (PostgresException)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
        #endregion

        #region READ
        public Game GetGameFromID(int id)
        {
            string stmt = "select id, is_won, number_of_tries, start_time, end_time, number_of_incorrect_tries, player_id, word_id from game where id = @id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Game game = null;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            game = new Game
                            {
                                Id = (int)reader["id"],
                                IsWon = (bool)reader["is_won"],
                                NumberOfTries = (int)reader["number_of_tries"],
                                StartTime = (DateTime)reader["start_time"],
                                EndTime = (DateTime)reader["end_time"],
                                NumberOfIncorrectTries = (int)reader["number_of_incorrect_tries"],
                                PlayerId = (int)reader["player_id"],
                                WordId = (int)reader["word_id"]
                            };
                        }
                    }
                    return game;
                }
            }
        }

        public Game GetGameFromPlayerID(int id)
        {
            string stmt = "select id, is_won, number_of_tries, start_time, end_time, number_of_incorrect_tries, player_id, word_id from game where id = @player_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Game game = null;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            game = new Game
                            {
                                Id = (int)reader["id"],
                                IsWon = (bool)reader["is_won"],
                                NumberOfTries = (int)reader["number_of_tries"],
                                StartTime = (DateTime)reader["start_time"],
                                EndTime = (DateTime)reader["end_time"],
                                NumberOfIncorrectTries = (int)reader["number_of_incorrect_tries"],
                                PlayerId = (int)reader["player_id"],
                                WordId = (int)reader["word_id"]
                            };
                        }
                    }
                    return game;
                }
            }
        }

        public Game GetGameFromWordID(int id)
        {
            string stmt = "select id, is_won, number_of_tries, start_time, end_time, number_of_incorrect_tries, player_id, word_id from game where id = @word_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Game game = null;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            game = new Game
                            {
                                Id = (int)reader["id"],
                                IsWon = (bool)reader["is_won"],
                                NumberOfTries = (int)reader["number_of_tries"],
                                StartTime = (DateTime)reader["start_time"],
                                EndTime = (DateTime)reader["end_time"],
                                NumberOfIncorrectTries = (int)reader["number_of_incorrect_tries"],
                                PlayerId = (int)reader["player_id"],
                                WordId = (int)reader["word_id"]
                            };
                        }
                    }
                    return game;
                }
            }
        }



        #endregion

        #region DELETE
        public void DeleteGame(int id)
        {
            string stmt = "DELETE FROM game WHERE id = @id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteScalar();
                }
            }
        }
        #endregion
    }
}
