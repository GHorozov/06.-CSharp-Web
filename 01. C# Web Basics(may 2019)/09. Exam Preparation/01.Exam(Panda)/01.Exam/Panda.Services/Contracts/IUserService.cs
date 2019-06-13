using Panda.Models;
using System.Collections.Generic;

namespace Panda.Services.Contracts
{
    public interface IUserService
    {
        User CreateUser(User user);

        User GetUserByusernameAndPassword(string name, string password);

        ICollection<string> AllUsernames();
    }
}
