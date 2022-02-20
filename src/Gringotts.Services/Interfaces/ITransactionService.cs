using Gringotts.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateAsync(int customerId, int accountNumber, CreateTransactionRequest createAccountRequest);
        Task<TransactionResponse> GetByIdAsync(int customerId, int accountNumber, int transactionId);
        Task<List<TransactionResponse>> GetAllAsync(int customerId, int accountNumber, DateTime start, DateTime end);
    }
}
