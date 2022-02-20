using Gringotts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Models.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> IsValidCustomerToCreate(Customer customer);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
    }
}
