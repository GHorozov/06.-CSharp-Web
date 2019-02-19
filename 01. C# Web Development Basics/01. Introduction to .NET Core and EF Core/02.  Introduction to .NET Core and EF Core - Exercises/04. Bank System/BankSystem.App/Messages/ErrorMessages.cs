using System.Runtime.Serialization;

namespace BankSystem.App.Messages
{
    public static class ErrorMessages
    {
        public const string UserExists = "User with same name already exists!";
        public const string LoginFirst = "Cannot log out. No user was logged in.";
        public const string LogoutFirst = "Logout first!";
        public const string IncorrectUsernamePassword = "Incorrect username / password";
        public const string TypeCorrectUsernamePassword = "Type correct username and password!";
        public const string IncorrectUsername = "Incorrect username";
        public const string IncorrectPassword = "Incorrect password";
        public const string IncorrectEmail = "Incorrect email";
        public const string IncorrectInput = "Incorrect input!";
        public const string AccountDontExist = "User doesnt have account with number {0}";
    }
}
