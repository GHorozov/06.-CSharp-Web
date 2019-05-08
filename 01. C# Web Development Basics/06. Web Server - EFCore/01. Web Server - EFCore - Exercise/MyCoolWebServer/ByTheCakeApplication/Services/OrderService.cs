namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.MyOrders;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;

    public class OrderService : IOrderService
    {
        public IEnumerable<MyOrdersListViewModel> GetAllOrdersByUserId(int userId)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db
                    .Orders
                    .Where(x => x.UserId == userId)
                    .Select(x => new MyOrdersListViewModel()
                    {
                        OrderId = x.Id,
                        CreatedOn = x.CreationDate,
                        Sum = x
                            .Products
                            .Sum(p => p.Product.Price)
                    })
                    .ToList();
            }
        }

        public OrderDetailsViewModel GetOrderDetails(int orderId)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db
                    .Orders
                    .Where(x => x.Id == orderId)
                    .Select(x => new OrderDetailsViewModel()
                    {
                        ProductId = x.Id,
                        CreatedOn = x.CreationDate,
                        Products = x.Products
                            .Where(op => op.OrderId == orderId)
                            .Select(op => new ProductListingViewModel()
                            {
                                Id = op.Product.Id,
                                Name = op.Product.Name,
                                Price = op.Product.Price
                            })
                            .ToList()
                           
                    })
                    .FirstOrDefault();
            }
        }

        public Order GetOrderByOrderId(int orderId)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db
                    .Orders
                    .Where(x => x.Id == orderId)
                    .FirstOrDefault();
            }
        }
    }
}
