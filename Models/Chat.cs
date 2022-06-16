using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool SendNotification { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
