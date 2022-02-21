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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ITransactionDal _transactionDal;

        public TransactionRepository(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }
        public async Task<Transaction> CreateAsync(Transaction transactionRequest, int accountNumber)
        {
            var result = await _transactionDal.CreateAsync(transactionRequest.ToDto(accountNumber));
            return result?.ToModel();
        }

        public async Task<List<Transaction>> GetAllAsync(int accountNumber, DateTime start, DateTime end)
        {
            var result = await _transactionDal.GetAllAsync(accountNumber, start, end);
            return result.Select(x => x.ToModel()).ToList();
        }

        public async Task<Transaction> GetByIdAsync(int accountNumber, int transactionId)
        {
            var result = await _transactionDal.GetByIdAsync(accountNumber, transactionId);
            return result?.ToModel();
        }
    }
}
