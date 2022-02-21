using Gringotts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL.Interfaces
{
    public interface IAccountDal
    {
        Task<Account> CreateAsync(Account account);
        Task<Account> GetByIdAsync(int customerId, int accountNumber);
        Task<List<Account>> GetAllByCustomerIdAsync(int customerId);
        Task<Account> UpdateAsync(Account account);
    }
}
