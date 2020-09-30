using Hangman.Models;
using Hangman.Repositories;
using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public int PlayerId { get; set; }
        public List<Message> Messages { get; set; }
        public MessageRepository MessageRepository { get; set; }


        public MessagesViewModel(int playerId)
        {
            MessageRepository = new MessageRepository();
            Messages = MessageRepository.GetMessages(playerId).ToList();
        }
    }
}
