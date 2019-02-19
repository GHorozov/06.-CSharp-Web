namespace BankSystem.Models
{
    using System;

    public class BaseAccount
    {
        public BaseAccount(int userId, string accountNumber, decimal balance)
        {
            this.UserId = userId;
            this.AccountNumber = accountNumber;
            this.Balance = balance;
        }

        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public void DepositMoney(decimal money)
        {
            this.Balance += money;
        }

        public void WithdrawMoney(decimal money)
        {
            var diff = this.Balance - money;
            if(diff >= 0)
            {
                this.Balance -= money;
            }
            else
            {
                throw new ArgumentException("Insufficient funds!");
            }
        }
    }
}
