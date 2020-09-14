using Hangman.Models;
using Microsoft.VisualBasic.CompilerServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hangman.Repositories
{
    public static class MessageRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["dbTest"].ConnectionString;

        #region READ
        public static IEnumerable<Message> GetMessages(int? recieverId = null, int? senderId = null)
        {
            string query = "select message.id, topic, content, sender_id, sender.name as sender_name, reciever_id, reciever.name as reciever_name, sent_at, read_at from message" +
                "\ninner join player sender on sender.id=sender_id" +
                "\ninner join player reciever on reciever.id=reciever_id" +
                $"{(senderId.HasValue ? $"\nwhere sender_id=@senderid" : recieverId.HasValue ? $"\nwhere reciever_id=@recieverid" : string.Empty)}" +
                $"{(senderId.HasValue && recieverId.HasValue ? $"\nand reciever_id=@recieverid" : string.Empty)}" +
                "\norder by sent_at";

            List<Message> result = new List<Message>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(query, conn))
                {
                    if (senderId.HasValue)
                    {
                        command.Parameters.AddWithValue("senderid", senderId.Value);
                    }
                    if (recieverId.HasValue)
                    {
                        command.Parameters.AddWithValue("recieverid", recieverId.Value);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return result;
                        }

                        while (reader.Read())
                        {
                            var row = new Message
                            {
                                Id = (int)reader["id"],
                                Topic = (string)reader["topic"],
                                Content = (string)reader["content"],
                                SenderName = (string)reader["sender_name"],
                                RecieverName = (string)reader["reciever_name"],
                                SentAt = (DateTime)reader["sent_at"],
                                ReadAt = reader["read_at"] == DBNull.Value ? null : (DateTime?)reader["read_at"]
                            };

                            result.Add(row);
                        }
                    }
                }
            }

            return result;
        }

        public static Message GetSingleMessage(int id)
        {
            var query = "select message.id, topic, content, sender_id, sender.name as sender_name, reciever_id, reciever.name as reciever_name, sent_at, read_at from message" +
                "\ninner join player sender on sender.id=sender_id" +
                "\ninner join player reciever on reciever.id=reciever_id" +
                "\nwhere message.id=@messageid";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        reader.Read();

                        return new Message
                        {
                            Id = (int)reader["id"],
                            Topic = (string)reader["topic"],
                            Content = (string)reader["content"],
                            SenderId = (int)reader["sender_id"],
                            SenderName = (string)reader["sender_name"],
                            RecieverId = (int)reader["reciever_id"],
                            RecieverName = (string)reader["reciever_name"],
                            SentAt = (DateTime)reader["sent_at"],
                            ReadAt = (DateTime)reader["read_at"],
                        };
                    }
                }
            }
        }
        #endregion

        public static bool TryAddMessage(Message message, out Message added)
        {
            return TryAddMessage(message.Topic, message.Content, message.SenderId, message.RecieverId, out added);
        }

        public static bool TryAddMessage(string topic, string content, int senderId, int recieverId, out Message added)
        {
            added = null;

            if (string.IsNullOrWhiteSpace(topic) 
                || string.IsNullOrWhiteSpace(content) 
                || senderId == 0 
                || recieverId == 0)
            {
                return false;
            }

            string query = "insert into message(topic, content, sent_at, sender_id, reciever_id) values (@topic, @content, CURRENT_TIMESTAMP, @senderId, @recieverId)" +
                "returning message.id, message.sent_at";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                try
                {
                    using (var command = new NpgsqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("topic", topic);
                        command.Parameters.AddWithValue("content", content);
                        command.Parameters.AddWithValue("senderId", senderId);
                        command.Parameters.AddWithValue("recieverId", recieverId);

                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            int id = (int)reader["id"];
                            if (id < 1)
                            {
                                return false;
                            }

                            var sentAt = (DateTime)reader["sent_at"];

                            added = new Message
                            {
                                Id = id,
                                Topic = topic,
                                Content = content,
                                SenderId = senderId,
                                RecieverId = recieverId,
                                SentAt = sentAt
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
