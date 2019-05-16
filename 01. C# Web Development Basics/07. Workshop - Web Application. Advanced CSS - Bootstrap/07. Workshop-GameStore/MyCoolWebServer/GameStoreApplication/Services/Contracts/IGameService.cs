namespace MyCoolWebServer.GameStoreApplication.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using MyCoolWebServer.GameStoreApplication.Data.Models;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Admin;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Cart;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Home;

    public interface IGameService
    {
        void CreateGame(string title, string description, string imageUrl, decimal price, double size, string videoId, DateTime releaseDate);

        IEnumerable<AdminListGameViewModel> GetAllGames();

        Game GetGameById(int id);

        void EditGame(int id, string title, string description, string imageUrl, decimal price, double size, string videoId, DateTime releaseDate);

        void DeleteGame(int id);

        IEnumerable<HomeGameListViewModel> AllGamesList();

        IEnumerable<HomeGameListViewModel> AllGamesListByUserId(int id);

        bool IsExist(int id);

        ICollection<CartGameViewModel> FindGames(ICollection<int> gameIds);
    }
}
