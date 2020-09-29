using Hangman.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Hangman.Repositories;
using Hangman.Modules;
using System.Windows.Input;
using Hangman.Models;
using Npgsql;
using System.Windows;
using System.Linq;


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
        public MessageRepository messageRepository;
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
            messageRepository.TryAddMessage(Topic, Message, ActivePlayer.Id, 64, out Message message);
            GetMessages();
            Message = "";
            Topic = "";
            Confirmation = "Ditt meddelande är skickat";
        }

        private void GetMessages()
        {
            myMessages = messageRepository.GetMessages(null, ActivePlayer.Id).ToList();
        }
        #endregion

    }
}
