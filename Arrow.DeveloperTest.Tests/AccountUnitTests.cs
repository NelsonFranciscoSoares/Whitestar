using Arrow.DeveloperTest.DomainExceptions;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Arrow.DeveloperTest.Tests
{
    public class AccountUnitTests
    {
        [Fact]
        public void AccountHasPositiveBalance_WithdrawWithAmountLowerThanAccountBalance_AccountBalanceIsPossitive()
        {
            //Arrange
            var account = new Account
            {
                AccountNumber = "3000",
                Balance = 10000
            };

            var amountValue = 2000;

            //Act
            account.Withdraw(amountValue);

            //Assert
            Assert.True(account.Balance == 8000);
        }

        [Fact]
        public void AccountHasPositiveBalance_WithdrawWithGreaterThanAccountBalance_RaisesException()
        {
            //Arrange
            var account = new Account
            {
                AccountNumber = "3000",
                Balance = 10000
            };

            var amountValue = 12000;

            //Act
            Action action = () => account.Withdraw(amountValue);

            //Assert
            Assert.Throws<WithdrawOperationException>(action);
        }
    }
}
