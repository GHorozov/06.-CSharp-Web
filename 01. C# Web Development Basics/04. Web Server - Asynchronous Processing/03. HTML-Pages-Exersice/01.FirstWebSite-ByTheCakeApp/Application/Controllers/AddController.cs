namespace _01.FirstWebSite_ByTheCakeApp.Application.Controllers
{
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;
    using _01.FirstWebSite_ByTheCakeApp.Application.Models;

    public class AddController
    {
        public HttpResponse AddGet()
        {
            return new ViewResponse(HttpResponceCode.OK, new AddView());
        }

        public HttpResponse AddPost(string name, string price)
        {
            var addView = new AddView();
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(price))
            {
                addView.ErrorString = "Invalid input!";

                return new ViewResponse(HttpResponceCode.OK, addView);
            }

            var priceInput = decimal.Parse(price);
            Cake cake = null;
            if (!string.IsNullOrWhiteSpace(name) && priceInput >= 0)
            {
                cake = new Cake(name, priceInput);
                CakeList.AddCake(cake);
            }

            if(cake != null)
            {
                return new ViewResponse(HttpResponceCode.OK, new AddView(cake));
            }

            return new ViewResponse(HttpResponceCode.OK, new AddView());
        }
    }
}
