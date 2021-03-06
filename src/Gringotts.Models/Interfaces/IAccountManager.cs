using Gringotts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Models.Interfaces
{
    public interface IAccountManager
    {
        Task<Account> CreateAsync(int customerId, Account account);
        Task<Account> GetByIdAsync(int customerId, int accountId);
        Task<List<Account>> GetAllAsync(int customerId);
    }
}
