namespace MyCoolWebServer.GameStoreApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyCoolWebServer.GameStoreApplication.Data;
    using MyCoolWebServer.GameStoreApplication.Data.Models;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Admin;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Home;

    public class GameService : IGameService
    {
        public void CreateGame(string title, string description, string imageUrl, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game()
                {
                    Title = title,
                    Description = description,
                    ImageUrl = imageUrl,
                    TrailerId = videoId,
                    Price = price,
                    Size = size,
                    ReleaseDate = releaseDate,
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<AdminListGameViewModel> GetAllGames()
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Select(x => new AdminListGameViewModel()
                    {
                        Id = x.Id,
                        Name = x.Title,
                        Price = x.Price,
                        Size = x.Size
                    })
                    .ToList();
            }
        }

        public Game GetGameById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Where(x => x.Id == id)
                    .Select(x => new Game()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Price = x.Price,
                        Size = x.Size,
                        ImageUrl = x.ImageUrl,
                        TrailerId = x.TrailerId,
                        ReleaseDate = x.ReleaseDate
                    })
                    .FirstOrDefault();
            }
        }

        public void EditGame(int id, string title, string description, string imageUrl, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var entity = db.Games.Where(x => x.Id == id).First();
                entity.Title = title;
                entity.Description = description;
                entity.ImageUrl = imageUrl;
                entity.Price = price;
                entity.Size = size;
                entity.ReleaseDate = releaseDate;
                entity.TrailerId = videoId;

                db.Games.Update(entity);
                db.SaveChanges();
            }
        }

        public void DeleteGame(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db
                    .Games
                    .Where(x => x.Id == id)
                    .First();

                db.Games.Remove(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<HomeGameListViewModel> AllGamesList()
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Select(x => new HomeGameListViewModel()
                    {
                        Id= x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        Price = x.Price,
                        Size = x.Size,
                        VideoId = x.TrailerId,
                        ReleaseDate = x.ReleaseDate
                    })
                    .ToList();
            }
        }

        public IEnumerable<HomeGameListViewModel> AllGamesListByUserId(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .UserGames
                    .Where(x => x.UserId == id)
                    .Select(x => x.Game)
                    .Select(x => new HomeGameListViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        Price = x.Price,
                        Size = x.Size,
                        VideoId = x.TrailerId,
                        ReleaseDate = x.ReleaseDate
                    })
                    .ToList();
            }
        }
    }
}
