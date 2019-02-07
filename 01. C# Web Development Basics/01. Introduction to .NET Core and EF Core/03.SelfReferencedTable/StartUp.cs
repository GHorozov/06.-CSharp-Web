namespace _03.SelfReferencedTable
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new MyDBContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var department = new Department() { Name = "Sales" };
            var employee1 = new Employee() { Name = "Ivan" };
            var employee2 = new Employee() { Name = "Georgi" };

            department.Employees.Add(employee1);
            department.Employees.Add(employee2);
            employee2.Employees.Add(employee1);

            context.Departments.Add(department);

            context.SaveChanges();
        }
    }
}
