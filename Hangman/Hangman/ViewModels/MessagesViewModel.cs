using Hangman.Database;
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

        private readonly IMessageRepository _messageRepository;
        public MessagesViewModel()
        {
            _messageRepository = new MessageRepository();
            Messages = _messageRepository.GetMessages(ActivePlayer.Id).ToList();
        }
    }
}
