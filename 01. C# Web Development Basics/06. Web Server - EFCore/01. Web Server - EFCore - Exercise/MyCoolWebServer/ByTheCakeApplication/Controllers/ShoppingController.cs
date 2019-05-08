namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Services;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingController : Controller
    {
        private IProductService productService;
        private IUserService userService;
        private IShoppingService shoppingService;

        public ShoppingController()
        {
            this.productService = new ProductService();
            this.userService = new UserService();
            this.shoppingService = new ShoppingService();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var isExist = this.productService.Exists(id);
            if (!isExist)
            {
                return new NotFoundResponse();
            }

            var userShoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            userShoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";
            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var totalCost = 0.00m;

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.ProductIds.Any())
            {
                var productsInCart = this.productService.FindProductInCart(shoppingCart.ProductIds);

                totalCost = productsInCart.Sum(x => x.Price);

                var sb = new StringBuilder();
                foreach (var product in productsInCart)
                {
                    sb.AppendLine($"<div>{product.Name} - ${product.Price:f2}</div>");
                }

                this.ViewData["products"] = sb.ToString();
                this.ViewData["totalCost"] = totalCost.ToString("f2");

                return FileViewResponse(@"shopping\cart");
            }

            this.ViewData["products"] = "No items in your cart.";
            this.ViewData["totalCost"] = totalCost.ToString();

            return FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.userService.GetUserByUsername(username);
            if(userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist!");
            }

            if (!shoppingCart.ProductIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shoppingService.CreateOrder(userId.Value, shoppingCart.ProductIds);

            shoppingCart.ProductIds.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
