namespace BankSystem.App.Sessions.Contracts
{
    using BankSystem.Models;

    public interface IUserSession
    {
        User User { get; set; }

        bool IsLoggedIn();

        void Login(User user);

        void Logout();
    }
}
