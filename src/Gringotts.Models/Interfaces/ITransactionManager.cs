using Gringotts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Models.Interfaces
{
    public interface ITransactionManager
    {
        Task<Transaction> CreateAsync(int customerId, int accountNumber, Transaction transactionRequest);
        Task<Transaction> GetByIdAsync(int customerId, int accountNumber, int transactionId);
        Task<List<Transaction>> GetAllAsync(int customerId, int accountNumber, DateTime start, DateTime end);
    }
}
