namespace MyWebServer.Server.HTTP.Response
{
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;
    using MyWebServer.Server.Enums;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse() 
            : base(HttpResponceCode.NotFound, new NotFoundView())
        {
        }
    }
}
