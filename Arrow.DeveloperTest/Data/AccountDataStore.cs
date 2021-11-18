using Arrow.DeveloperTest.Types;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Arrow.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        private readonly List<Account> _accounts;

        public AccountDataStore()
        {
            _accounts = this.CreateListAccount();
        }

        public Account GetAccount(string accountNumber)
        {
            return this._accounts
                    .SingleOrDefault(param => param.AccountNumber == accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            this._accounts
                    .RemoveAll(predicate => predicate.AccountNumber == account.AccountNumber);

            this._accounts
                    .Add(account);
        }

        private List<Account> CreateListAccount()
        {
            var accounts = new List<Account>
            {
                new Account
                {
                    AccountNumber = "1000",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                    Balance = 1000,
                    Status = AccountStatus.Live
                },
                new Account
                {
                    AccountNumber = "2000",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Balance = 1000,
                    Status = AccountStatus.Live
                },
                 new Account
                {
                    AccountNumber = "3000",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                    Balance = 1000,
                    Status = AccountStatus.Live
                }
            };

            return accounts;
        }
    }
}
