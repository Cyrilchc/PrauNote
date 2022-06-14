using System;
using System.Collections.Generic;

namespace Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime GroupDate { get; set; }
        public int GroupTotal { get; set; }
        public List<Student> Students { get; set; }
    }
}
