using System;
using System.Collections.Generic;

namespace Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public double AssignmentGrade { get; set; }
        //public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }
}
