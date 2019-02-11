namespace _05.ShopHierarchy
{
    using System;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ShopDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedSalesmanTable(context);
            SeedCustomerTable(context);

            var result = context.Salesmans.OrderByDescending(x => x.Customers.Count).ThenBy(x => x.Name).ToArray();

            foreach (var salesman in result)
            {
                Console.WriteLine($"{salesman.Name} – {salesman.Customers.Count} customers");
            }
        }

        private static void SeedCustomerTable(ShopDbContext context)
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var line = input.Split(new [] { ";", "-"}, StringSplitOptions.RemoveEmptyEntries);
                var customerName = line[1];
                var salesmanId = int.Parse(line[2]);

                var currentCustomer = new Customer(customerName, salesmanId);
                context.Customers.Add(currentCustomer);
                context.SaveChanges();
            }
        }

        private static void SeedSalesmanTable(ShopDbContext context)
        {
            var input = Console.ReadLine().Split(";").ToArray();

            foreach (var name in input)
            {
                var currentSalesman = new Salesman(name);
                context.Salesmans.Add(currentSalesman);
            }

            context.SaveChanges();
        }
    }
}
