namespace _01.FirstWebSite_ByTheCakeApp.Application.Controllers
{
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;
    using _01.FirstWebSite_ByTheCakeApp.Application.Models;

    public class SearchController
    {
        public HttpResponse SearchGet()
        {
            return new ViewResponse(HttpResponceCode.OK, new SearchView());
        }

        public HttpResponse SearchPost(string name)
        {
            var result = CakeList.GetCakeByName(name);
            var searchView = new SearchView(result);
            if (string.IsNullOrWhiteSpace(name))
            {
                searchView.ErrorString = "Invalid input!";

                return new ViewResponse(HttpResponceCode.OK, searchView);
            }

            return new ViewResponse(HttpResponceCode.OK, searchView);
        }
    }
}
