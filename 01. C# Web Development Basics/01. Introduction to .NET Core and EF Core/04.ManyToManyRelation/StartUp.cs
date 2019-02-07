namespace _04.ManyToManyRelation
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MyDBContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var course = new Course() { Name = "C#" };
            var student = new Student() { Name = "Georgi" };
            var studentsCourses = new StudentsCourses() { StudentId = student.Id, Student = student, CourseId = course.Id, Course = course };

            context.Courses.Add(course);
            context.Students.Add(student);
            context.StudentsCourses.Add(studentsCourses);

            context.SaveChanges();
        }
    }
}
