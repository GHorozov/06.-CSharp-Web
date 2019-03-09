namespace MyWebServer.Server.HTTP.Response
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Enums;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl) 
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpResponceCode.Found;
            this.HeaderCollection.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}
