using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public double Score { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
