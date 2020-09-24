using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Database
{
    interface IMessageRepository
    {
        IEnumerable<Message> GetMessages(int? recieverId, int? senderId);
        Message GetSingleMessage(int id);
        bool TryAddMessage(Message message, out Message added);
        bool TryAddMessage(string topic, string content, int senderId, int recieverId, out Message added);
    }
}
