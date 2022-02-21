using Gringotts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL.Interfaces
{
    public interface ITransactionDal
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetByIdAsync(int accountNumber, int transactionId);
        Task<List<Transaction>> GetAllAsync(int accountNumber, DateTime start, DateTime end);
    }
}
