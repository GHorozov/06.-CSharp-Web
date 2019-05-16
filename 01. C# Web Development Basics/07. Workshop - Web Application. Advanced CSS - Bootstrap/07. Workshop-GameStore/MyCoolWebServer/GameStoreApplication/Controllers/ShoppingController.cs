namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.GameStoreApplication.ViewModels;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Linq;
    using System.Text;

    public class ShoppingController : BaseController
    {
        private const string CartViewConst = @"cart\cart";

        private IShoppingService shoppingService;
        private IGameService gameService;
        private IUserService userService;
        public ShoppingController(IHttpRequest request)
            : base(request)
        {
            this.shoppingService = new ShoppingService();
            this.gameService = new GameService();
            this.userService = new UserService();
        }

        public IHttpResponse AddToCart(int id)
        {
            var game = this.gameService.IsExist(id);
            if (!game)
            {
                this.ShowError("This game is not found.");

                return new RedirectResponse("/");
            }

            var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
            var userId = this.userService.GetUserId(email);
            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            var isAlreadyOwnGame = this.userService.IsUserOwnGame(userId, id); 
            if (shoppingCart.ProductIds.Contains(id) || isAlreadyOwnGame)
            {
                this.ShowError("You already buy this product.");

                return new RedirectResponse("/");
            }

            shoppingCart.ProductIds.Add(id);

            this.ShowSuccess("You succesfully buy product!");

            return new RedirectResponse("/");
        }

        public IHttpResponse ShowCart()
        {
            var totalPrice = 0.00m;

            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.ProductIds.Any())
            {
                var allGamesInCart = this.gameService.FindGames(shoppingCart.ProductIds);

                totalPrice = allGamesInCart.Sum(x => x.Price);

                var sb = new StringBuilder();
                foreach (var game in allGamesInCart)
                {
                    sb.AppendLine($@"
                         <div class=""media"">
                            <a class=""btn btn-outline-danger btn-lg align-self-center mr-3"" href=""/cart/delete/{game.Id}"">X</a>
                            
                            <img class=""d-flex mr-4 align-self-center img-thumbnail"" height=""127"" src=""{game.ImageUrl}""
                                 width=""227"" alt=""Generic placeholder image"">

                            <div class=""media-body align-self-center"">
                                <a href=""#"" >
                                    <h4 class=""mb-1 list-group-item-heading"">{game.Title}</h4>
                                </a>
                                <p>
                                    {game.Description}
                                </p>
                            </div>

                            <div class=""col-md-2 text-center align-self-center mr-auto"">
                                <h2> {game.Price}&euro; </h2>
                            </div>
                        </div>");
                }

                this.ViewData["games"] = sb.ToString();
                this.ViewData["totalPrice"] = totalPrice.ToString("f2");

                return FileViewResponse(CartViewConst);
            }

            this.ViewData["games"] = "No items in your cart.";
            this.ViewData["totalPrice"] = totalPrice.ToString("f2");

            return FileViewResponse(CartViewConst);
        }

        public IHttpResponse CartDelete(int id)
        {
            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.ProductIds.Remove(id);

            return ShowCart();
        }

        public IHttpResponse FinishOrder()
        {
            if (!this.Authentication.IsAuthenticated)
            {
                this.ShowError("You must login first!");

                return new RedirectResponse("/");
            }


            return null;
        }
    }
}
