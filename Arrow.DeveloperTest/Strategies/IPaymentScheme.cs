using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Strategies
{
    public interface IPaymentScheme
    {
        MakePaymentResult Pay(Account account, MakePaymentRequest paymentData);
    }
}
