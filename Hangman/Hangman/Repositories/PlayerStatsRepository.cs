using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Configuration;
using Hangman.Models;

namespace Hangman.Repositories
{
    class PlayerStatsRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        #region READ
        public static double GetGamesPlayed(IPlayer player)
        {
            string stmt = "Select COUNT(player_id) FROM Game where player_id=" + player.Id;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    
                        double count = Convert.ToDouble(command.ExecuteScalar());


                    return count;
                }

            }
        }

        public static double GetGamesWon(IPlayer player)
        {
            string stmt = "Select COUNT(player_id) FROM Game where player_id=" + player.Id + "and is_won = true";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {

                    double count = Convert.ToDouble(command.ExecuteScalar());


                    return count;
                }

            }
        }
        #endregion
    }
}
