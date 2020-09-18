using Hangman.Models;
using System.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

namespace Hangman.Repositories
{
    public static class Player_Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        #region CREATE

        //Metod för att skapa spelare - Vill vi ha name/id/Person som indata?
        public static void CreatePlayer(string name)
        {
            string stmt = "INSERT INTO player (name) values(@name) returning id";
            Player player = new Player();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {

                        using (var command = new NpgsqlCommand())
                        {
                            command.Parameters.AddWithValue("name", name);
                            command.Connection = conn;
                            command.CommandText = stmt;
                            command.Prepare();
                            int id = (int)command.ExecuteScalar();
                            command.Parameters.Clear();

                        }
                        trans.Commit();

                    }
                    //SQL state för redan existerande namn: 23505
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }

        #endregion

        #region READ

        //Metod för att läsa in alla användare 
        public static List<Player> GetPlayers()
        {
            string stmt = "SELECT name, id FROM player ORDER BY name";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                List<Player> players = new List<Player>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Name = (string)reader["name"],
                                Id = (int)reader["id"]
                            };

                            players.Add(player);
                        }
                    }
                }
                return players;
            }
        }

        //En metod för att hämta en användare - vill vi ha name/id/Player som indata?
        public static Player GetPlayer(string name)
        {
            string stmt = "SELECT id, name FROM player WHERE name=@name";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = new Player();
                player = null;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],

                            };
                        }
                    }
                    return player;
                }

            }
        }

        public static Player GetPlayerFromID(int id)
        {
            string stmt = "SELECT id, name FROM player WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = new Player();
                player = null;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],

                            };
                        }
                    }
                    return player;
                }

            }
        }
        #endregion

        #region DELETE

        //Metod för att ta bort en spelare - vill vi ha name/id/Person som indata?
        public static void DeletePlayer(int id)
        {
            string stmt = "DELETE FROM player WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    using (var command = new NpgsqlCommand(stmt, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("id", id);
                        command.ExecuteScalar();
                    }
                }
                catch (PostgresException)
                {
                    throw;
                }
            }
        }
        #endregion

        #region UPDATE
        public static void UpdateNameOnPlayer(string name, int id)
        {
            string stmt = "UPDATE player SET name = @name WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            command.Parameters.AddWithValue("id", id);
                            command.Parameters.AddWithValue("name", name);
                            command.ExecuteScalar();
                        }
                        trans.Commit();
                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}