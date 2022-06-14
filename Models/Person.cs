using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
