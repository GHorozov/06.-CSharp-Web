namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Models;
    using MyCoolWebServer.Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesController : Controller
    {
        private CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            return this.FileViewResponse(@"cakes\add", new Dictionary<string, string>()
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Add(string name, string price)
        {
            this.cakesData.Add(name, price);

            var result = this.FileViewResponse(@"cakes\add", new Dictionary<string, string>()
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });

            return result;
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchNameKey = "name";
            var results = string.Empty;
            var searchName = "Search cakes...";

            if (req.UrlParameters.ContainsKey(searchNameKey))
            {
                searchName = req.UrlParameters[searchNameKey];

                var searchedCakesDivs = this.cakesData
                    .All()
                    .Where(x => x.Name.ToLower().Contains(searchName.ToLower()))
                    .Select(x => $@"<div>{x.Name} - ${x.Price} <a href=""/shopping/add/{x.Id}?name={searchName}"">Order</a></div>");

                results = string.Join(Environment.NewLine, searchedCakesDivs);
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.Orders.Any())
            {
                var productsCount = shoppingCart
                    .Orders
                    .Count;

                var productsText = productsCount != 1 ? " products" : " product";

                return this.FileViewResponse(@"cakes\search", new Dictionary<string, string>()
                {
                    ["showCart"] = "block",
                    ["products"] = $"{productsCount}{productsText}",
                    ["results"] = results,
                    ["searchName"] = searchName
                });
            }

            return this.FileViewResponse(@"cakes\search", new Dictionary<string, string>()
            {
                ["showCart"] = "none",
                ["results"] = results,
                ["searchName"] = searchName
            });
        }
    }
}
