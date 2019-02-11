namespace _09.QueryLoadedData
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
            SeedItems(context);
            SeedTablesByCommand(context);

            PrintResult(context);
        }

        private static void PrintResult(ShopDbContext context)
        {
            var customerId = int.Parse(Console.ReadLine());

            var orderCount = context
                .Orders
                .Where(x => x.CustomerId == customerId)
                .Where(x => x.ItemsOrders.Count > 1)
                .Count();

            Console.WriteLine($"Orders: {orderCount}");
        }

        private static void SeedItems(ShopDbContext context)
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var line = input.Split(";");
                var itemName = line[0];
                var itemPrice = decimal.Parse(line[1]);

                var currentItem = new Item(itemName, itemPrice);
                context.Items.Add(currentItem);
                context.SaveChanges();
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

                        for (int i = 2; i < line.Length; i++)
                        {
                            var itemId = int.Parse(line[i]);

                            currentOrder.ItemsOrders.Add(new ItemsOrders() { ItemId = itemId, OrderId = currentOrder.Id });
                        }

                        context.Orders.Add(currentOrder);
                        context.SaveChanges();
                        break;

                    case "review":
                        customerId = int.Parse(line[1]);
                        var inputItemId = int.Parse(line[2]);

                        var currentReview = new Review(customerId, inputItemId);
                        context.Reviews.Add(currentReview);
                        context.SaveChanges();
                        break;
                    default:
                        throw new InvalidOperationException("Invalid input!");
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
