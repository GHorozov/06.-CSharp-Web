namespace _06.ShopHierarchyExtended
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
            SeedTablesByCommand(context);

            var allCustomersOrdered = context.Customers
                .OrderByDescending(x => x.Orders.Count)
                .ThenByDescending(x => x.Reviews.Count)
                .ToArray();

            foreach (var customer in allCustomersOrdered)
            {
                var customerName = customer.Name;
                var ordersCount = customer.Orders.Count;
                var reviewsCount = customer.Reviews.Count;

                Console.WriteLine(customerName);
                Console.WriteLine($"Orders: {ordersCount}");
                Console.WriteLine($"Reviews: {reviewsCount}");
            }
        }

        private static void SeedTablesByCommand(ShopDbContext context)
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var line = input.Split(new[] { ";", "-" }, StringSplitOptions.RemoveEmptyEntries);
                var command = line[0];

                switch (command)
                {
                    case "register":
                        var customerName = line[1];
                        var salesmanId = int.Parse(line[2]);

                        var currentCustomer = new Customer(customerName, salesmanId);
                        context.Customers.Add(currentCustomer);
                        context.SaveChanges();
                        break;

                    case "order":
                        var customerId = int.Parse(line[1]);
                        var currentOrder = new Order(customerId);
                        context.Orders.Add(currentOrder);
                        context.SaveChanges();
                        break;
                    case "review":
                        customerId = int.Parse(line[1]);
                        var currentReview = new Review(customerId);
                        context.Reviews.Add(currentReview);
                        context.SaveChanges();
                        break;
                    default:
                        break;
                }
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
