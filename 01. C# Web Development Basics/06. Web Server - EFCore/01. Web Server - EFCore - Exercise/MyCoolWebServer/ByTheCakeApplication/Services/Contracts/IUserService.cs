namespace MyCoolWebServer.ByTheCakeApplication.Services.Contracts
{
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Account;

    public interface IUserService
    {
        bool Create(string username, string password);

        bool Find(string username, string password);

        ProfileViewModel GetProfile(string username);

        int? GetUserByUsername(string username);
    }
}
