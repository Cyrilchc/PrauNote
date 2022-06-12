namespace Models
{
    public class Grade
    {
        public int Id { get; set; }
        public double AssignmentGrade { get; set; }
        public Assignment Assignment { get; set; }
    }
}
