using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string TextMessage { get; set; }
        public Person From { get; set; }
        public Person To { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
