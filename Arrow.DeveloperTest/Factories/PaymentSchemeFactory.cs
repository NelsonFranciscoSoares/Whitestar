using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Strategies;
using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Factories
{
    public class PaymentSchemeFactory : IPaymentSchemeFactory
    {
        private readonly IAccountDataStore _accountDataStore;

        public PaymentSchemeFactory(IAccountDataStore accountDataStore)
        {
            this._accountDataStore = accountDataStore;
        }

        public IPaymentScheme Create(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsPaymentScheme(this._accountDataStore);
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentsPaymentScheme(this._accountDataStore);
                case PaymentScheme.Chaps:
                    return new ChapsPaymentScheme(this._accountDataStore);
                default:
                    throw new PaymentSchemeUnsupportedException();
            }
        }
    }
}
