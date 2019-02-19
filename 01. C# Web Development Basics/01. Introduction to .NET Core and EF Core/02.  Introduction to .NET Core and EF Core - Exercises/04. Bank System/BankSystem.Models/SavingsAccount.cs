namespace BankSystem.Models
{
    public class SavingsAccount : BaseAccount
    {
        public SavingsAccount(int userId, string accountNumber, decimal balance, decimal interestRate) 
            : base(userId, accountNumber, balance)
        {
            this.InterestRate = interestRate;
        }

        public decimal InterestRate { get; set; }

        public void AddInterest()
        {
            this.Balance += (this.Balance * this.InterestRate);
        }
    }
}
