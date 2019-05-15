namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Admin;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System;
    using System.Linq;

    public class AdminController : BaseController
    {
        private const string AddViewConst = @"admin\addGame";
        private const string ListViewConst = @"admin\listGames";
        private const string EditViewConst = @"admin\editGame";
        private const string DeleteViewConst = @"admin\deleteGame";

        private IGameService gameService; 

        public AdminController(IHttpRequest request) 
            : base(request)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse Add()
        {
            if (this.Authentication.IsAdmin)
            {
                return this.FileViewResponse(AddViewConst);
            }

            return new RedirectResponse("/");
        }

        public IHttpResponse Add(AdminAddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            if (!this.ValidateModel(model))
            {
                return this.Add(); 
            }

            this.gameService.CreateGame(model.Title, model.Description, model.ImageUrl, model.Price, model.Size, model.TrailerId, model.ReleaseDate.Value);

            return new RedirectResponse("/admin/games/list");
        }

        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            var results = this.gameService
                .GetAllGames()
                .Select(x => $@"
                    <tr class=""table - warning"">
                    <th scope = ""row"">{x.Id}</th>
                    <td>{x.Name}</td>
                    <td>{x.Size:f2} GB</td>
                    <td>{x.Price:f2} &euro;</td>
                    <td>
                       <a href = ""/admin/games/edit/{x.Id}"" class=""btn btn-warning btn-sm"">Edit</a>
                       <a href = ""/admin/games/delete/{x.Id}"" class=""btn btn-danger btn-sm"">Delete</a>
                    </td>
                    </tr>");


            this.ViewData["rowsContent"] = string.Join(Environment.NewLine, results);

            return this.FileViewResponse(ListViewConst);
        }

        public IHttpResponse Edit(int id)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            var game = this.gameService.GetGameById(id);
            if(game == null)
            {
                return new RedirectResponse("/admin/games/list");
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["imageUrl"] = game.ImageUrl;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["videoId"] = game.TrailerId;
            this.ViewData["release-date"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            return this.FileViewResponse(EditViewConst);
        }

        public IHttpResponse Edit(AdminEditGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            if (!this.ValidateModel(model))
            {
                this.ShowError("Invaid input!");

                return new RedirectResponse($"/admin/games/edit/{model.Id}");
            }

            this.gameService.EditGame(model.Id, model.Title, model.Description, model.ImageUrl, model.Price, model.Size, model.TrailerId, model.ReleaseDate.Value);

            return new RedirectResponse("/admin/games/list");
        }

        public IHttpResponse Delete(int id)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            var game = this.gameService.GetGameById(id);
            if (game == null)
            {
                return new RedirectResponse("/admin/games/list");
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["imageUrl"] = game.ImageUrl;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["videoId"] = game.TrailerId;
            this.ViewData["release-date"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            return this.FileViewResponse(DeleteViewConst);
        }

        public IHttpResponse DeleteGame(int id)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            var game = this.gameService.GetGameById(id);
            if(game == null)
            {
                return new RedirectResponse("/admin/games/list");
            }

            this.gameService.DeleteGame(id);

            return new RedirectResponse("/admin/games/list");
        }
    }
}
