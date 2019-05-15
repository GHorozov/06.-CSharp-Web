namespace MyCoolWebServer.GameStoreApplication.Services.Contracts
{
    using MyCoolWebServer.GameStoreApplication.ViewModels.Account;

    public interface IUserService
    {
        bool CreateUser(RegisterViewModel model);

        bool Find(LoginViewModel model);

        bool IsAdmin(string email);

        int GetUserId(string email);
    }
}
