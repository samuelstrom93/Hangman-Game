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
        public static Word GetRandomWord()
        {
            string stmt = $"SELECT id, name, hint FROM word ORDER BY RANDOM() LIMIT 1";
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
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Hint = (string)reader["hint"],
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
