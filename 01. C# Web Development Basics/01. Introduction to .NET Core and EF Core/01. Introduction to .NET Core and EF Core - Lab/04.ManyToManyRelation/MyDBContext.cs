namespace _04.ManyToManyRelation
{
    using System;
    using Microsoft.EntityFrameworkCore;
   
    public class MyDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentsCourses> StudentsCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=Task04;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentsCourses>()
                .HasKey(x => new { x.StudentId, x.CourseId });

            modelBuilder.Entity<StudentsCourses>()
                .HasOne<Student>(x => x.Student)
                .WithMany(x => x.StudentsCourses)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<StudentsCourses>()
                .HasOne<Course>(x => x.Course)
                .WithMany(x => x.StudentsCourses)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
