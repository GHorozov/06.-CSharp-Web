namespace MyCoolWebServer.ByTheCakeApplication.ViewModels
{
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "^Current_Shopping_Cart^";

        public ShoppingCart()
        {
            this.Orders = new List<AddProductViewModel>();
        }

        public List<AddProductViewModel> Orders { get; set; }
    }
}
