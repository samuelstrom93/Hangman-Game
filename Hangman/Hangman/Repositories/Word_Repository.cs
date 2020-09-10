using Hangman.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Hangman.Repositories
{
    class Word_Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;
        // randomId måste vara mellan 46-50 för att få en träff i databasen
        public static Word GetRandomWord(int randomId)
        {
            string stmt = $"SELECT name, description FROM word WHERE id = {randomId}";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Word word;
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            word = new Word
                            {
                                Id = randomId,
                                Name = (string)reader["name"],
                                Description = (string)reader["description"],
                            };
                            return word;
                        }
                    }
                }
            }
            return null;
        }



    }
}
