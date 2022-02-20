using Gringotts.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> CreateAsync(int customerId, CreateAccountRequest createAccountRequest);
        Task<AccountResponse> GetByIdAsync(int customerId, int accountNumber);
        Task<List<AccountResponse>> GetAllAsync(int customerId);
    }
}
