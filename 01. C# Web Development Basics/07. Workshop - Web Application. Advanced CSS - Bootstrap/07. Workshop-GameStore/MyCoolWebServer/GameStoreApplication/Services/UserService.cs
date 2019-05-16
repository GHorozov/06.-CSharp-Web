namespace MyCoolWebServer.GameStoreApplication.Services
{
    using MyCoolWebServer.GameStoreApplication.Data;
    using MyCoolWebServer.GameStoreApplication.Data.Models;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Account;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool CreateUser(RegisterViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(x => x.Email == model.Email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User()
                {
                    Name = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    IsAdmin = isAdmin
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(LoginViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(x => x.Email == model.Email && x.Password == model.Password);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(x => x.Email == email && x.IsAdmin == true);
            }
        }

        public int GetUserId(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Where(x => x.Email == email)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            }
        }

        public bool IsUserOwnGame(int userId, int gameId)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Where(x => x.Id == userId)
                    .Select(x => x.Games)
                    .Select(ug => ug.Any(g => g.GameId == gameId))
                    .First();
            }
        }
    }
}
