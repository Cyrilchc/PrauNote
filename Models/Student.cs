using System;

namespace Models
{
    public class Student
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
