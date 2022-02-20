using Gringotts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL.Interfaces
{
    public interface ICustomerDal
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> GetByNameAndMobileAsync(string name, string mobile);
        Task<Customer> GetByIdAsync(int customerId);
    }
}
