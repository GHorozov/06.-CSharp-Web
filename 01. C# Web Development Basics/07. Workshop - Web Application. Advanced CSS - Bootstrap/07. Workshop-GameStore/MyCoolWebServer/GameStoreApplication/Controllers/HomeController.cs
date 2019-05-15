namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        private const string HomeViewConst = @"home\index";
        private const string HomeUserViewConst = @"home\userHome";
        private IGameService gameService;
        private IUserService userService;

        public HomeController(IHttpRequest request)
            :base(request)
        {
            this.gameService = new GameService();
            this.userService = new UserService();
        }

        public IHttpResponse Index()
        {
            var allGames = this.gameService.AllGamesList().ToList();
            if (!allGames.Any())
            {
                this.ViewData["games"] = "Games not found.";

                return this.FileViewResponse(HomeViewConst);
            }

            var isAdmin = "none";
            if (this.Authentication.IsAuthenticated)
            {
                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
                var loggedInUser = this.userService.GetUserId(email);
                this.ViewData["userId"] = loggedInUser.ToString();

                if (this.Authentication.IsAdmin)
                {
                    isAdmin = "flex";
                }
            }
            else
            {
                this.ViewData["isDisable"] = "disabled";
            }

            var sb = new StringBuilder();
            for (int i = 0; i < allGames.Count(); i += 3)
            {
                sb.AppendLine(@"<div class=""card-group"">");

                var gamesCount = Math.Min(i + 3, allGames.Count());
                for (int j = i; j < gamesCount; j++)
                {
                    var currentGame = allGames[j];
                    
                    sb.AppendLine(@"<div class=""card col-4 thumbnail"">");
                    sb.AppendLine($@"<img
                                class=""card-image-top img-fluid img-thumbnail""
                                onerror = ""this.src='{currentGame.ImageUrl}';""
                                src = ""{currentGame.ImageUrl}"" > ");

                    sb.AppendLine($@"
                        <div class=""card-body"">
                            <h4 class=""card-title"">{currentGame.Title}</h4>
                            <p class=""card-text""><strong>Price</strong> - {currentGame.Price:f2}&euro;</p>
                            <p class=""card-text""><strong>Size</strong> - {currentGame.Size} GB</p>
                            <p class=""card-text"">{currentGame.Description}</p>
                        </div>");

                    sb.AppendLine($@"
                        <div class=""card-footer"">
                            <a style=""display: {isAdmin}"" class=""card-button btn btn-outline-warning"" name=""edit"" href=""/admin/games/edit/{currentGame.Id}"">Edit</a>
                            <a style=""display: {isAdmin}"" class=""card-button btn btn-outline-danger"" name=""delete"" href=""/admin/games/delete/{currentGame.Id}"">Delete</a>

                            <a class=""card-button btn btn-outline-primary"" name=""info"" href=""/home/games/details/{currentGame.Id}"">Info</a>
                            <a class=""card-button btn btn-primary"" name=""buy"" href=""/home/games/buy/{currentGame.Id}"">Buy</a>
                        </div>");

                    sb.AppendLine("</div>");
                }

                sb.AppendLine(@"</div>");
            }

           
            this.ViewData["games"] = sb.ToString();

            return this.FileViewResponse(HomeViewConst);
        }

        public IHttpResponse OwnedGames(int userId)
        {
            var isAdmin = "none";
            if (this.Authentication.IsAuthenticated)
            {
                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
                var loggedInUser = this.userService.GetUserId(email);
                this.ViewData["userId"] = loggedInUser.ToString();

                if (this.Authentication.IsAdmin)
                {
                    isAdmin = "flex";
                }
            }

            var gamesById = this.gameService.AllGamesListByUserId(userId).ToList();
            if(!gamesById.Any())
            {
                this.ViewData["games"]= "You do not have any games.";

                return this.FileViewResponse(HomeUserViewConst); 
            }

            var sb = new StringBuilder();
            for (int i = 0; i < gamesById.Count(); i += 3)
            {
                sb.AppendLine(@"<div class=""card-group"">");

                var gamesCount = Math.Min(i + 3, gamesById.Count());
                for (int j = i; j < gamesCount; j++)
                {
                    var currentGame = gamesById[j];

                    sb.AppendLine(@"<div class=""card col-4 thumbnail"">");
                    sb.AppendLine($@"<img
                                class=""card-image-top img-fluid img-thumbnail""
                                onerror = ""this.src='{currentGame.ImageUrl}';""
                                src = ""{currentGame.ImageUrl}"" > ");

                    sb.AppendLine($@"
                        <div class=""card-body"">
                            <h4 class=""card-title"">{currentGame.Title}</h4>
                            <p class=""card-text""><strong>Price</strong> - {currentGame.Price:f2}&euro;</p>
                            <p class=""card-text""><strong>Size</strong> - {currentGame.Size} GB</p>
                            <p class=""card-text"">{currentGame.Description}</p>
                        </div>");

                    sb.AppendLine($@"
                        <div class=""card-footer"">
                            <a style=""display: {isAdmin}"" class=""card-button btn btn-outline-warning"" name=""edit"" href=""/admin/games/edit/{currentGame.Id}"">Edit</a>
                            <a style=""display: {isAdmin}"" class=""card-button btn btn-outline-danger"" name=""delete"" href=""/admin/games/delete/{currentGame.Id}"">Delete</a>

                            <a class=""card-button btn btn-outline-primary"" name=""info"" href=""/home/games/details/{userId}"">Info</a>
                            <a class=""card-button btn btn-primary"" name=""buy"" href=""/home/games/buy/{userId}"">Buy</a>
                        </div>");

                    sb.AppendLine("</div>");
                }

                sb.AppendLine(@"</div>");
            }

            this.ViewData["games"] = sb.ToString();

            return this.FileViewResponse(HomeUserViewConst);
        }

    }
}
