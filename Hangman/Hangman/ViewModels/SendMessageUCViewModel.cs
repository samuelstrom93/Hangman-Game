using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using System.Windows.Input;
using Hangman.Models;
using System.Linq;
using Hangman.Database;
using Hangman.Database;

namespace Hangman.ViewModels
{
    public class SendMessageUCViewModel : BaseViewModel
    {
        #region Properties: Message
        public ICommand SendMessageCommand { get; set; }
        public List<Message> myMessages { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string Confirmation { get; set; }
        #endregion

        #region Repos
        private readonly IMessageRepository messageRepository;
        #endregion

        public SendMessageUCViewModel()
        {
            messageRepository = new MessageRepository();
            SendMessageCommand = new RelayCommand(SendMessage);
            GetMessages();
        }

        #region Methods: Messages
        private void SendMessage()
        {
            if (IsContentNotNull())
            {
                messageRepository.TryAddMessage(Topic, Message, ActivePlayer.Id, 64, out Message message);
                GetMessages();
                Message = null;
                Topic = null;
                Confirmation = "Ditt meddelande är skickat";
            }
            else
                Confirmation = "Du har inte fyllt i alla fält";
        }

        public bool IsContentNotNull()
        {
            if (Message == null || Topic == null)
                return false;
            else
                return true;
        }

        private void GetMessages()
        {
            myMessages = messageRepository.GetMessages(null, ActivePlayer.Id).ToList();
        }
        #endregion

    }
}
