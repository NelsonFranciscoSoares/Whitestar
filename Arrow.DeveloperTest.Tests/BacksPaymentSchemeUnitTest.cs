using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Factories;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Strategies;
using Arrow.DeveloperTest.Types;
using Moq;
using System;
using Xunit;

namespace Arrow.DeveloperTest.Tests
{
    public class BacksPaymentSchemeUnitTest
    {
        [Fact]
        public void AccountBalancePermitsBacsPayment_BacksPaymentRequest_WithdrawIsDone()
        {
            //Arrange
            var account = new Account
            {
                AccountNumber = "3000",
                Balance = 10000,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };
            var makePaymentRequest = new MakePaymentRequest
            {
                Amount = 1000,
                DebtorAccountNumber = "3000",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Bacs
            };

            Mock<IAccountDataStore> mock = new Mock<IAccountDataStore>();
            mock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var paymentService = new PaymentService(new PaymentSchemeFactory(mock.Object),mock.Object);

            //Act
            var makePaymentResponse = paymentService.MakePayment(makePaymentRequest);

            //Assert
            Assert.True(makePaymentResponse.Success);
            Assert.True(account.Balance == 9000);
        }
    }
}
