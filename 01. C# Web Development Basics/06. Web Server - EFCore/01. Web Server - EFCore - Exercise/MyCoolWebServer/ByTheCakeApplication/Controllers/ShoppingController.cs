namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingController : Controller
    {
        private CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var cake = this.cakesData.Find(id);
            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var userShoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            userShoppingCart.Orders.Add(cake);

            var redirectUrl = "/search";
            const string searchTermKey = "name";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }


            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var totalCost = 0.00m;

            var allCakes = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders;
            if (allCakes.Any())
            {
                totalCost = allCakes.Sum(x => x.Price);

                var sb = new StringBuilder();
                foreach (var cake in allCakes)
                {
                    sb.AppendLine($"<div>{cake.ToString()}</div>");
                }

                this.ViewData["cakes"] = sb.ToString();
                this.ViewData["totalCost"] = totalCost.ToString("f2");

                return FileViewResponse(@"shopping\cart");
            }

            this.ViewData["cakes"] = "No items in your cart.";
            this.ViewData["totalCost"] = totalCost.ToString();

            return FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
