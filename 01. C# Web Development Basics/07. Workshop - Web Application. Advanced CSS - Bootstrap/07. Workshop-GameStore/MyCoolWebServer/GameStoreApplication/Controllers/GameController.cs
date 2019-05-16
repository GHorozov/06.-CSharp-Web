namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;

    public class GameController : BaseController
    {
        private const string GameDetailsViewConst = @"game\details";

        private IGameService gameService;
        public GameController(IHttpRequest request)
            : base(request)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse Details(int gameId)
        {
            var isAdmin = "none";
            if (this.Authentication.IsAdmin)
            {
               isAdmin = "inline";
            }

            var game = this.gameService.GetGameById(gameId);
            if (game == null)
            {
                ShowError("This game is not found.");

                return new RedirectResponse("/");
            }

            this.ViewData["isAdmin"] = isAdmin;
            this.ViewData["id"] = game.Id.ToString();
            this.ViewData["title"] = game.Title;
            this.ViewData["videoId"] = game.TrailerId;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["releaseDate"] = game.ReleaseDate.ToString();
                

            return this.FileViewResponse(GameDetailsViewConst);
        }
    }
}
