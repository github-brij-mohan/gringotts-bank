using Gringotts.DAL.Entities;
using Gringotts.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL
{
    public class AccountDal : IAccountDal
    {
        private readonly BankDbContext _bankDbContext;

        public AccountDal(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
            _bankDbContext.Database.EnsureCreated();
        }

        public async Task<Account> CreateAsync(Account account)
        {
            _bankDbContext.Accounts.Add(account);
            await _bankDbContext.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAllByCustomerIdAsync(int customerId)
        {
            return _bankDbContext.Accounts.Where(x => x.CustomerId == customerId).ToList();
        }

        public async Task<Account> GetByIdAsync(int customerId, int accountNumber)
        {
            var result = _bankDbContext.Accounts.Where(x => x.CustomerId == customerId && x.AccountNumber == accountNumber)?.FirstOrDefault();
            return result;
        }
    }
}
