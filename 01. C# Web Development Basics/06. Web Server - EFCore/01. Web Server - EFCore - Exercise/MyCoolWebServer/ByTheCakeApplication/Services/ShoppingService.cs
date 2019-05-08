namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order()
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = ids
                        .Select(x => new OrderProduct()
                        {
                            ProductId = x
                        })
                        .ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}
