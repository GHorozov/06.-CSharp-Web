using Panda.Data;
using Panda.Models;
using Panda.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class UserService : IUserService
    {
        private readonly PandaDbContext context;

        public UserService(PandaDbContext context)
        {
            this.context = context;
        }

        public User CreateUser(User user)
        {
            User userResult = this.context
                .Users
                .Add(user)
                .Entity;

            this.context.SaveChanges();

            return userResult;
        }

        public User GetUserByusernameAndPassword(string username, string password)
        {
            var user = this.context
                .Users
                .SingleOrDefault(x => x.Username == username && x.Password == password);

            return user;
        }

        public ICollection<string> AllUsernames()
        {
            var users = this.context
                .Users
                .Select(x => x.Username)
                .ToList();

            return users; 
        }
    }
}
