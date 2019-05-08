namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Services;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
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
            const string searchTermKey = "searchTerm";
            var urlParameters = req.UrlParameters;

            var searchTerm = urlParameters.ContainsKey(searchTermKey)
               ? urlParameters[searchTermKey]
               : null;

            var resultProducts = this.productService.All(searchTerm);

            if (!resultProducts.Any())
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allProducts = resultProducts
                    .Select(x => $@"<div><a href=""/productDetails/{x.Id}"">{x.Name}</a> ${x.Price} <a href=""/shopping/add/{x.Id}?name={searchTerm}"">Order</a></div>");

                var allProductsString = string.Join(Environment.NewLine, allProducts);

                this.ViewData["results"] = allProductsString;
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.ProductIds.Any())
            {
                var productsCount = shoppingCart
                    .ProductIds
                    .Count;

                var productsText = productsCount != 1 ? " products" : " product";

                this.ViewData["showCart"] = "block";
                this.ViewData["cartProducts"] = $"{productsCount}{productsText}";
                this.ViewData["searchTerm"] = searchTerm;

                return this.FileViewResponse(@"products\search");
            }
            else
            {
                this.ViewData["showCart"] = "none";
                this.ViewData["searchTerm"] = searchTerm;

                return this.FileViewResponse(@"products\search");
            }
        }

        public IHttpResponse Details(int id)
        {
            var product = this.productService.GetById(id);
            if(product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["productName"] = product.Name;
            this.ViewData["productPrice"] = product.Price.ToString();
            this.ViewData["imageUrl"] = product.ImageUrl;

            return this.FileViewResponse(@"products\details");
        }
    }
}
