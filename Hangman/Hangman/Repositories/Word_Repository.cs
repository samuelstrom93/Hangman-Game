using Hangman.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Hangman.Repositories
{
    public class Word_Repository
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["dbMain"].ConnectionString;

        #region READ
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
        #endregion

        public static bool TryAddWord(string word, string hint, out Word addedWord)
        {
            addedWord = null;
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(hint))
            {
                return false;
            }

            string query = "INSERT INTO word(name, hint) VALUES (@word, @hint) returning id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                try
                {
                    using (var command = new NpgsqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("word", word);
                        command.Parameters.AddWithValue("hint", hint);

                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            int id = (int)reader["id"];
                            if (id < 1)
                            {
                                return false;
                            }

                            addedWord = new Word
                            {
                                Id = id,
                                Name = word,
                                Hint = hint,
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    // TODO
                    return false;
                }
            }

            return true;
        }

    }
}
