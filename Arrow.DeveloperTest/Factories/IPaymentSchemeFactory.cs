using Arrow.DeveloperTest.Strategies;
using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Factories
{
    public interface IPaymentSchemeFactory
    {
        public IPaymentScheme Create(PaymentScheme paymentScheme);
    }
}
