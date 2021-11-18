using Arrow.DeveloperTest.DomainExceptions;

namespace Arrow.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        public bool CanWithdraw(decimal amount)
        {
            return amount <= this.Balance;
        }

        public void Withdraw(decimal amount)
        {
            if (CanWithdraw(amount))
            {
                this.Balance -= amount;
                return;
            }

            throw new WithdrawOperationException();
        }

        public bool IsLive()
        {
            return this.Status == AccountStatus.Live;
        }

        public bool CanMakeTransaction(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            return this.AllowedPaymentSchemes.HasFlag(allowedPaymentSchemes);
        }
    }
}
