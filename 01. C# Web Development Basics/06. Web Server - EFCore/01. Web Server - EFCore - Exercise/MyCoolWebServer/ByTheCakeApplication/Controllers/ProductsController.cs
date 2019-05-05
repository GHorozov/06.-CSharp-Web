namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Services;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using MyCoolWebServer.Server.Http.Contracts;
    using System;
    using System.Linq;

    public class ProductsController : Controller
    {
        private IProductService productService;

        public ProductsController()
        {
            this.productService = new ProductService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";

            return this.FileViewResponse(@"products\add");
        }

        public IHttpResponse Add(AddProductViewModel model)
        {
            if (model.Name.Length < 3 || model.Name.Length > 30 || model.ImageUrl.Length < 3)
            {
                this.AddError("Incorect product input details.");

                return this.FileViewResponse(@"products\add");
            }

            this.productService.Create(model.Name, model.Price, model.ImageUrl);

            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = model.Price.ToString();
            this.ViewData["imageUrl"] = model.ImageUrl;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"products\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchNameKey = "name";
            var results = string.Empty;
            var searchName = "Search cakes...";

            if (req.UrlParameters.ContainsKey(searchNameKey))
            {
                searchName = req.UrlParameters[searchNameKey];

                //var searchedCakesDivs = this.cakesData
                //    .All()
                //    .Where(x => x.Name.ToLower().Contains(searchName.ToLower()))
                //    .Select(x => $@"<div>{x.Name} - ${x.Price} <a href=""/shopping/add/{x.Id}?name={searchName}"">Order</a></div>");

                //results = string.Join(Environment.NewLine, searchedCakesDivs);
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.Orders.Any())
            {
                var productsCount = shoppingCart
                    .Orders
                    .Count;

                var productsText = productsCount != 1 ? " products" : " product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{productsCount}{productsText}";
                this.ViewData["results"] = results;
                this.ViewData["searchName"] = searchName;

                return this.FileViewResponse(@"products\search");
            }

            this.ViewData["showCart"] = "none";
            this.ViewData["results"] = results;
            this.ViewData["searchName"] = searchName;

            return this.FileViewResponse(@"products\search");
        }
    }
}
