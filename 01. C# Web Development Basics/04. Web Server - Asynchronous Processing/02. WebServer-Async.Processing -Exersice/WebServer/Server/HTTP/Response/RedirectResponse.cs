namespace WebServer.Server.HTTP.Response
{
    using WebServer.Server.Common;
    using WebServer.Server.Enums;


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
