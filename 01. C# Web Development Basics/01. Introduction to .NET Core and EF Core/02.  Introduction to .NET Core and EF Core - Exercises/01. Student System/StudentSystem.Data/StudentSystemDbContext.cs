namespace StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;

    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<StudentCourse> StudentCourse { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=StudentSystem;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<StudentCourse>()
                .HasKey(x => new { x.StudentId, x.CourseId });

            modelBuilder
                .Entity<Student>()
                .HasMany(x => x.Courses)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            modelBuilder
               .Entity<Course>()
               .HasMany(x => x.Students)
               .WithOne(x => x.Course)
               .HasForeignKey(x => x.CourseId);

            modelBuilder
                .Entity<Resource>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Resources)
                .HasForeignKey(x => x.CourseId);

            modelBuilder
                .Entity<Homework>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Homeworks)
                .HasForeignKey(x => x.CourseId);

            modelBuilder
                .Entity<Homework>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Homeworks)
                .HasForeignKey(x => x.StudentId);

            modelBuilder
                .Entity<License>()
                .HasOne(x => x.Resource)
                .WithMany(x => x.Licenses)
                .HasForeignKey(x => x.ResourceId);
        }
    }
}
