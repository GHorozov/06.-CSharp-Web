namespace MyCoolWebServer.ByTheCakeApplication.ViewModels.MyOrders
{
    using System;

    public class MyOrdersListViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Sum { get; set; }
    }
}
