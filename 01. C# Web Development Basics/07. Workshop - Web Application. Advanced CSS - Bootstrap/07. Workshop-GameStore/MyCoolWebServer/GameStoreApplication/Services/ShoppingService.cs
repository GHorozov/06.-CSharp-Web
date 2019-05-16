namespace MyCoolWebServer.GameStoreApplication.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using MyCoolWebServer.GameStoreApplication.Data;
    using MyCoolWebServer.GameStoreApplication.Data.Models;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> ids)
        {
            using(var db = new GameStoreDbContext())
            {
                var order = new Order()
                {
                    CreationDate = DateTime.UtcNow,
                    UserId = userId,
                    Games = ids
                      .Select(x => new OrderGame()
                      {
                          GameId = x
                      })
                      .ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}
