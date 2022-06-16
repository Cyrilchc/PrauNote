using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public int FromPersonId { get; set; }
        [ForeignKey("FromPersonId")]
        public virtual Person From { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public int ChatId { get; set; }
    }
}
