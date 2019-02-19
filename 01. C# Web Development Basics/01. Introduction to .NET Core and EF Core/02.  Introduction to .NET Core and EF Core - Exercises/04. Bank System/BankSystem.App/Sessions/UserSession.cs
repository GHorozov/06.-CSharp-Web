namespace BankSystem.App.Sessions
{
    using BankSystem.App.Sessions.Contracts;
    using BankSystem.Models;

    public class UserSession : IUserSession
    {
        public User User { get; set; }

        public bool IsLoggedIn()
        {
            if(this.User == null)
            {
                return false;
            }

            return true;
        }

        public void Login(User user)
        {
            this.User = user;
        }

        public void Logout()
        {
            this.User = null;
        }
    }
}
