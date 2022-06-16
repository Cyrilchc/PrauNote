using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Student : Person
    {
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
