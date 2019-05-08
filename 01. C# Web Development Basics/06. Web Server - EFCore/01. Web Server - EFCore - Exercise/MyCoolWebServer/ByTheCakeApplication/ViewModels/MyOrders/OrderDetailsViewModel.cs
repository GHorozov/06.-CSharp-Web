namespace MyCoolWebServer.ByTheCakeApplication.ViewModels.MyOrders
{
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using System;
    using System.Collections.Generic;

    public class OrderDetailsViewModel
    {
        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ProductListingViewModel> Products { get; set; } = new List<ProductListingViewModel>();
    }
}
