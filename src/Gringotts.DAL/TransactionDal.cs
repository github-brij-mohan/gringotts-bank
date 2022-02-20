using Gringotts.DAL.Entities;
using Gringotts.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL
{
    public class TransactionDal : ITransactionDal
    {
        private readonly BankDbContext _bankDbContext;

        public TransactionDal(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
            _bankDbContext.Database.EnsureCreated();
        }
        public async Task<Transaction> CreateTransactionAndUpdateAccountBalanceAsync(Transaction transaction, double balance)
        {
            _bankDbContext.Transactions.Add(transaction);

            var account = _bankDbContext.Accounts.Find(transaction.AccountNumber);
            account.Balance = balance;

            _bankDbContext.Accounts.Update(account);
            await _bankDbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>> GetAllAsync(int accountNumber, DateTime start, DateTime end)
        {
            return _bankDbContext.Transactions.Where(x => x.AccountNumber == accountNumber && x.Time >= start && x.Time <= end).ToList();
        }

        public async Task<Transaction> GetByIdAsync(int accountNumber, int transactionId)
        {
            return _bankDbContext.Transactions.Where(x => x.AccountNumber == accountNumber && x.Id == transactionId)?.FirstOrDefault();
        }
    }
}
