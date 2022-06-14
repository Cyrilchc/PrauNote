using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public List<Person> Attendees { get; set; }
        public List<Message> Messages { get; set; }
    }
}