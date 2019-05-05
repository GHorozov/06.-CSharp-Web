namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Account;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(x => x.Username == username))
                {
                    return false;
                }

                var user = new User(username, password);
                db.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public bool Find(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.Any(x => x.Username == username && x.Password == password);
            }
        }

        public ProfileViewModel GetProfile(string username)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db
                    .Users
                    .Where(x => x.Username == username)
                    .Select(x => new ProfileViewModel()
                    {
                        Username = x.Username,
                        RegistrationDate = x.RegistrationDate,
                        TotalOrders = x.Orders.Count()
                    })
                    .FirstOrDefault();
            }
        }
    }
}
