using Panda.Data;
using Panda.Models;
using Panda.Services.Contracts;

namespace Panda.Services
{
    public class UserService : IUserService
    {
        private readonly PandaDbContext context;

        public UserService(PandaDbContext runesDbContext)
        {
            this.context = runesDbContext;
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
    }
}
