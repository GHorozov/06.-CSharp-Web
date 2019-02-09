namespace _02.OneToManyRelation
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MyDBContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var department = new Department() { Name = "Sales"};
            var employee = new Employee() { Name = "Georgi" };
            department.Employees.Add(employee);
            context.Departments.Add(department);

            context.SaveChanges();
        }
    }
}
