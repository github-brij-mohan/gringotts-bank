using Gringotts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Models.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transactionRequest, int accountNumber);
        Task<Transaction> GetByIdAsync(int accountNumber, int transactionId);
        Task<List<Transaction>> GetAllAsync(int accountNumber, DateTime start, DateTime end);
    }
}
