namespace BankSystem.Models
{
    public class CheckingAccount : BaseAccount
    {
        public CheckingAccount(int userId, string accountNumber, decimal balance, decimal fee) 
            : base(userId, accountNumber, balance)
        {
            this.Fee = fee;
        }

        public decimal Fee { get; set; }

        public void DeductFee()
        {
            this.Balance -= this.Fee;
        }
    }
}
