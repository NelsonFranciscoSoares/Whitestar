using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.DomainExceptions;
using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Strategies
{
    public class FasterPaymentsPaymentScheme : IPaymentScheme
    {
        private readonly IAccountDataStore _accountDataStore;

        public FasterPaymentsPaymentScheme(IAccountDataStore accountDataStore)
        {
            this._accountDataStore = accountDataStore;
        }

        public MakePaymentResult Pay(Account account, MakePaymentRequest paymentData)
        {
            if (account == null) throw new AccountNotExistsException();

            var makePaymentResult = new MakePaymentResult();

            if (!account.CanMakeTransaction(AllowedPaymentSchemes.FasterPayments)
                || !account.CanWithdraw(paymentData.Amount))
            {
                makePaymentResult.Success = false;
                return makePaymentResult;
            }

            account.Withdraw(paymentData.Amount);
            _accountDataStore.UpdateAccount(account);
            makePaymentResult.Success = true;

            return makePaymentResult;
        }
    }
}
