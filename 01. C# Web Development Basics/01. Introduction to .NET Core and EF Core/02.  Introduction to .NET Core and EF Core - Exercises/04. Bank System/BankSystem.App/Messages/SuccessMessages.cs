namespace BankSystem.App.Messages
{
    public static class SuccessMessages
    {
        public const string UserRegistered = "{0} was registered in the system";
        public const string UserLoggedIn = "Succesfully logged in {0}";
        public const string UserLogout = "User {0} successfully logged out";
        public const string CreatedAccount = "Succesfully added account with number {0}";
        public const string DepositMoney = "Account {0} has balance of {1:f2}";
        public const string WithdrawMoney = "Account {0} has balance of {1:f2}";
        public const string AddInterest = "Added interest to {0}. Current balance: {1:f2}";
        public const string DeductFee = "Deducted fee of {0}. Current balance: {1:f2}";
    }
}
