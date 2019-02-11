namespace StudentSystem.App
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private const int TotalStudentsCount = 20;
        private const int TotalCoursesCount = 10;
        private const int TotalStudentInCoursesCount = 10;
        private static Random random = new Random();
        private const int ResourceCountNumber = 5;
        private const int TotalLicensesCount = 10;

        public static void Main()
        {
            var context = new StudentSystemDbContext();

            InitializeDatabase(context);

            SeedDatabase(context);

            SeedDatabaseWithLicenses(context);

            PrintStudentsWithHomeworks(context);

            PrintAllCoursesWithResources(context);

            PrintCoursesWithMoreThanFiveResorces(context);

            PrintCoursesActiveOnDate(context);

            PrintCalculationForEachStudent(context);

            PrintAllCoursesWithResourcesAndLicenses(context);

            PrintAllStudentsWithResourcesAndLicenses(context);
        }

        private static void InitializeDatabase(StudentSystemDbContext context)
        {
            context.Database.Migrate();
        }

        private static void SeedDatabase(StudentSystemDbContext context)
        {
            //Students
            Console.WriteLine("Adding students.");
            var allStudents = new List<Student>();
            for (int i = 0; i < 20; i++)
            {
                var student = new Student()
                {
                    Name = $"Student {i}",
                    PhoneNumber = $"0888 {i}2{i} 3{i}4",
                    RegisteredOn = DateTime.Now.AddDays(i),
                    BirthDay = DateTime.Now.AddYears(-20).AddDays(i),
                };

                allStudents.Add(student);
                context.Students.Add(student);
            }

            context.SaveChanges();

            //Courses
            Console.WriteLine("Adding courses.");
            var allCourses = new List<Course>();
            for (int i = 1; i <= TotalCoursesCount; i++)
            {
                var course = new Course()
                {
                    Name = $"Course {i}",
                    Description = $"Course details {i}",
                    StartDate = DateTime.Now.AddDays(i),
                    EndDate = DateTime.Now.AddDays(10 + i),
                    Price = 90 + i
                };

                allCourses.Add(course);
                context.Courses.Add(course);
            }

            context.SaveChanges();

            //StudentCourse
            Console.WriteLine("Adding students in courses.");
            for (int i = 0; i < TotalStudentInCoursesCount; i++)
            {
                var randomStudentId = random.Next(1, TotalStudentsCount);
                var randomCourseId = random.Next(1, TotalCoursesCount);

                var studentCourse = new StudentCourse(randomStudentId, randomCourseId );

                allStudents[randomStudentId].Courses.Add(studentCourse);
                allCourses[randomCourseId].Students.Add(studentCourse);

                context.StudentCourse.Add(studentCourse);

                var resourseInCourse = random.Next(1, TotalCoursesCount);
                var types = new int[] { 1, 2, 3, 999};

                var resource = new Resource()
                {
                    Name = $"Resource {i}",
                    Url = $"Url {i}",
                    ResourceType = (ResourceType)types[random.Next(1, types.Length)],
                };

                context.Resources.Add(resource);
                allCourses[randomCourseId].Resources.Add(resource);
            }

            context.SaveChanges();


            //Resourses
            Console.WriteLine("Adding resourses.");

            //Homework
            Console.WriteLine("Adding homeworks.");
            var contentTypes = new int[] { 0, 1, 2 };
            for (int i = 0; i < TotalCoursesCount; i++)
            {
                var currentCourse = allCourses[i];
                var studentIds = currentCourse.Students.Select(x => x.StudentId).ToList();

                for (int j = 0; j < studentIds.Count; j++)
                {
                    var totalHomeWork = random.Next(2, 5);

                    for (int k = 0; k < totalHomeWork; k++)
                    {
                        context.Homeworks.Add(new Homework
                        {
                            Content = $"Content tetails {i}",
                            ContentType = (ContentType)contentTypes[random.Next(0, contentTypes.Length)],
                            CourseId = currentCourse.Id,
                            Course = currentCourse,
                            StudentId = studentIds[j],
                            SubmissionDate = DateTime.Now.AddDays(-i)
                        });
                    }
                }

                context.SaveChanges();
            }

            context.SaveChanges();
        }

        private static void SeedDatabaseWithLicenses(StudentSystemDbContext context)
        {
            var allResources = context.Resources.ToArray();
            for (int i = 0; i < TotalLicensesCount; i++)
            {
                var license = new License()
                {
                    Name = $"License name {i}",
                };

                var randomResourceId = random.Next(1, ResourceCountNumber);
                allResources[randomResourceId].Licenses.Add(license);

                context.Licenses.Add(license);
            }

            context.SaveChanges();
        }

        private static void PrintStudentsWithHomeworks(StudentSystemDbContext context)
        {
            var students = context
                .Students
                .Select(x => new
                {
                    Name = x.Name,
                    Homeworks = x.Homeworks.Select(h => new
                    {
                        Content = h.Content,
                        ContentType = h.ContentType
                    })
                })
                .OrderBy(x => x.Name)
                .ToArray();

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine($"    --Content: {homework.Content}, ContentType: {homework.ContentType}");
                }
            }
        }

        private static void PrintAllCoursesWithResources(StudentSystemDbContext context)
        {
            var courses = context
                .Courses
                .Select(x => new
                {
                    Name = x.Name,
                    Description = x.Description,
                    Resources = x.Resources,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                })
                .OrderBy(x => x.StartDate)
                .ThenBy(x => x.EndDate)
                .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name}");
                Console.WriteLine($"    Description: {course.Description}");
                Console.WriteLine($"    Resources:");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"        --{resource.Name}");
                    Console.WriteLine($"        --{resource.Url}");
                    Console.WriteLine($"        --{resource.ResourceType}");
                }
            }
        }

        private static void PrintCoursesWithMoreThanFiveResorces(StudentSystemDbContext context)
        {
            var courses = context
               .Courses
               .Where(x => x.Resources.Count > ResourceCountNumber)
               .Select(x => new
               {
                   Name = x.Name,
                   ResourceCount = x.Resources.Count,
                   StartDate = x.StartDate
               })
               .OrderByDescending(x => x.ResourceCount)
               .ThenByDescending(x => x.StartDate)
               .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - ResourceCount: {course.ResourceCount}");
            }
        }

        private static void PrintCoursesActiveOnDate(StudentSystemDbContext context)
        {
            DateTime TargetDate = DateTime.Now.AddDays(2);

            var courses = context
               .Courses
               .Where(x => x.StartDate.Day == TargetDate.Day)
               .Select(x => new
               {
                   Name = x.Name,
                   StartDate = x.StartDate,
                   EndDate = x.EndDate,

                   EnrollStudents = x.Students.Count,
                   Duration = (int)(x.EndDate.Subtract(x.StartDate).TotalDays)
               })
               .OrderByDescending(x => x.EnrollStudents)
               .ThenByDescending(x => x.Duration)
               .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name}");
                Console.WriteLine($"    --StartDate: {course.StartDate}");
                Console.WriteLine($"    --EndDate: {course.EndDate}");
                Console.WriteLine($"    --Duration: {course.Duration} days");
                Console.WriteLine($"    --Enrolled students: {course.EnrollStudents}");
            }
        }

        private static void PrintCalculationForEachStudent(StudentSystemDbContext context)
        {
            var students = context
                .Students
                .Where(x => x.Courses.Any())
                .Select(x => new
                {
                    StudentName = x.Name,
                    EnrolledCourses = x.Courses.Count,
                    TotalSumPaid = x.Courses.Sum(c => c.Course.Price),
                    AveragePriceForCourse = x.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(x => x.TotalSumPaid)
                .ThenByDescending(x => x.EnrolledCourses)
                .ThenBy(x => x.StudentName)
                .ToArray();

            foreach (var student in students)
            {
                if(student.EnrolledCourses != 0)
                {
                Console.WriteLine($"{student.StudentName}");
                Console.WriteLine($"    --Number of enrolled courses: {student.EnrolledCourses}");
                Console.WriteLine($"    --Total price paid: $ {student.TotalSumPaid}");
                Console.WriteLine($"    --Average price paid for course: $ {student.AveragePriceForCourse.ToString("f2")}");
                }
            }
        }

        private static void PrintAllCoursesWithResourcesAndLicenses(StudentSystemDbContext context)
        {
            var courses = context
                .Courses
                .OrderByDescending(x => x.Resources.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    Resources = x.Resources
                    .OrderByDescending(res => res.Licenses.Count)
                    .ThenBy(res => res.Name)
                    .Select(r => new
                    {
                        r.Name,
                        LicensesNames = r.Licenses.Select(n => n.Name).ToList()
                    })
                })
                .ToArray();

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
                foreach (var resource in course.Resources)
                {
                    Console.Write($"--Resourse name: {resource.Name} -> ");
                    if (resource.LicensesNames.Any())
                    {
                        Console.WriteLine($"{string.Join(", ", resource.LicensesNames)}");
                    }
                    else
                    {
                        Console.WriteLine("none");
                    }
                }
            }
        }

        private static void PrintAllStudentsWithResourcesAndLicenses(StudentSystemDbContext context)
        {
            var students = context
                .Students
                .Where(x => x.Courses.Any())
                .Select(x => new
                {
                    x.Name,
                    CoursesCount = x.Courses.Count,
                    TotalResourcesCount = x.Courses.Sum(r => r.Course.Resources.Count),
                    TotalLicensesCount = x.Courses.Sum(r => r.Course.Resources.Sum(l => l.Licenses.Count()))
                })
                .OrderByDescending(x => x.CoursesCount)
                .OrderByDescending(x => x.TotalResourcesCount)
                .OrderBy(x => x.Name)
                .ToArray();

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} -> courses: {student.CoursesCount} resources: {student.TotalResourcesCount} licenses: {student.TotalLicensesCount}");
            }
        }
    }
}
