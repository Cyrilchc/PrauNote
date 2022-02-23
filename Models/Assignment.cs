using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public Student Student { get; set; }
        //public double Grade { get; set; }
        public Subject Subject { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
