namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Services;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.MyOrders;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;

    public class MyOrdersController : Controller
    {
        private IUserService userService;
        private IOrderService orderService;

        public MyOrdersController()
        {
            this.userService = new UserService();
            this.orderService = new OrderService();
        }

        public IHttpResponse MyOrders(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var userId = this.userService.GetUserByUsername(username);
            if(userId == null)
            {
                throw new InvalidOperationException($"User with {username} does not exist!");
            }

            var orders = this.orderService.GetAllOrdersByUserId(userId.Value);
            if (!orders.Any())
            {
                return new RedirectResponse("/");
            }

            var row = new StringBuilder();
            foreach (var order in orders)
            {
                row.AppendLine($@"
<tr style=""border: solid 1px;"">
    <td style=""border: solid 1px;""> <a href=""/orderDetails/{order.OrderId}""> {order.OrderId} </a> </td> 
    <td style=""border: solid 1px;"">{order.CreatedOn.ToShortDateString()}</td> 
    <td style=""border: solid 1px;"">{order.Sum}</td> 
</tr>");
            }

            this.ViewData["tableRow"] = row.ToString();

            return this.FileViewResponse(@"myOrders\myOrders");
        }

        public IHttpResponse OrderDetails(int orderId)
        {
            var order = (OrderDetailsViewModel)this.orderService.GetOrderDetails(orderId);
            if (order == null ||
                !order.Products.Any())
            {
                return new NotFoundResponse();
            }

            var row = new StringBuilder();
            foreach (var product in order.Products)
            {
                row.AppendLine($@"
<tr>
    <td style=""border: solid 1px;""> <a href=""/productDetails/{product.Id}""> {product.Name} </a> </td> 
    <td style=""border: solid 1px;""> ${product.Price:f2}</td> 
</tr>");
            }

            this.ViewData["orderId"] = order.ProductId.ToString();
            this.ViewData["tableRow"] = row.ToString();
            this.ViewData["createdOn"] = order.CreatedOn.ToShortDateString();

            return this.FileViewResponse(@"myOrders\details");
        }
    }
}
