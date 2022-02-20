using Gringotts.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateAsync(CreateCustomerRequest customerRequest);
        Task<CustomerResponse> GetByIdAsync(int customerId);
    }
}
