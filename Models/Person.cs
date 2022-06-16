using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Discriminator { get; set; }
        public int? GroupID { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
