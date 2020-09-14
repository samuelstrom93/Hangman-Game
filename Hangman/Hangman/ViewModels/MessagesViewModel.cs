using Hangman.Models;
using Hangman.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.ViewModels
{
    public class MessagesViewModel
    {
        public int PlayerId { get; set; }
        public List<Message> Messages { get; set; }

        public MessagesViewModel(int playerId)
        {
            Messages = MessageRepository.GetMessages(playerId).ToList();
        }
    }
}
