using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            var isValidCustomerToCreate = await _customerRepository.IsValidCustomerToCreateAsync(customer);
            if (!isValidCustomerToCreate)
            {
                throw new Exception("Invalid Customer Details.");
            }

            return await _customerRepository.CreateAsync(customer); 
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if(customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists.");
            }

            return customer;
        }
    }
}
