namespace StudentSystem.Models
{
    public class StudentCourse
    {
        public StudentCourse(int studentId, int courseId)
        {
            this.StudentId = studentId;
            this.CourseId = courseId;
        }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
