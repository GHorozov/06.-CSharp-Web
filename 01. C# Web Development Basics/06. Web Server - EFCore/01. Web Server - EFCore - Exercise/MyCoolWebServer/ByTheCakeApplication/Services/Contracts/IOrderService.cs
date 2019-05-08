namespace MyCoolWebServer.ByTheCakeApplication.Services.Contracts
{
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.MyOrders;
    using System.Collections.Generic;

    public interface IOrderService
    {
        IEnumerable<MyOrdersListViewModel> GetAllOrdersByUserId(int userId);

        OrderDetailsViewModel GetOrderDetails(int orderId);

        Order GetOrderByOrderId(int orderId);
    }
}
