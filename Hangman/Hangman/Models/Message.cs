using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Topic { get; set; }
        public int RecieverId { get; set; }
        public string RecieverName { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

    }
}
