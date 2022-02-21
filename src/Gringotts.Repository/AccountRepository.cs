using Gringotts.Core.Models;
using Gringotts.DAL.Interfaces;
using Gringotts.Models.Interfaces;
using Gringotts.Repository.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountDal _accountDal;

        public AccountRepository(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }

        public async Task<Account> CreateAsync(int customerId, Account account)
        {
            var result = await _accountDal.CreateAsync(account.ToDto(customerId));
            return result.ToModel();
        }

        public async Task<List<Account>> GetAllByCustomerIdAsync(int customerId)
        {
            var result = await _accountDal.GetAllByCustomerIdAsync(customerId);
            return result.Select(x => x.ToModel()).ToList();
        }

        public async Task<Account> GetByIdAsync(int customerId, int accountNumber)
        {
            var result = await _accountDal.GetByIdAsync(customerId, accountNumber);
            return result.ToModel();
        }

        public async Task<Account> UpdateAccountBalanceAsync(int customerId, int accountNumber, double balance)
        {
            var account =  await _accountDal.GetByIdAsync(customerId, accountNumber);
            account.Balance = balance;
            account = await _accountDal.UpdateAsync(account);
            return account.ToModel();
        }
    }
}
