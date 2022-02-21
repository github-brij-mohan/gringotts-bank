using Gringotts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Models.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(int customerId, Account account);
        Task<Account> GetByIdAsync(int customerId, int accountNumber);
        Task<List<Account>> GetAllByCustomerIdAsync(int customerId);
        Task<Account> UpdateAccountBalanceAsync(int customerId, int accountNumber, double balance);
    }
}
