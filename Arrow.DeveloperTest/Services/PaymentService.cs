using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Factories;
using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentSchemeFactory _paymentSchemeFactory;
        private readonly IAccountDataStore _accountDataStore;

        public PaymentService(
            IPaymentSchemeFactory paymentSchemeFactory,
            IAccountDataStore accountDataStore)
        {
            this._paymentSchemeFactory = paymentSchemeFactory;
            this._accountDataStore = accountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = this._accountDataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null) return new MakePaymentResult { Success = false };

            var paymentSchemeService = _paymentSchemeFactory.Create(request.PaymentScheme);
            return paymentSchemeService.Pay(account, request);
        }
    }
}
