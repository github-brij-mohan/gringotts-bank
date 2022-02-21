using Gringotts.Core.Models;
using Gringotts.DAL;
using Gringotts.DAL.Interfaces;
using Gringotts.Models.Interfaces;
using Gringotts.Repository.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDal _customerDto;
        public CustomerRepository(ICustomerDal customerDto)
        {
            _customerDto = customerDto;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            var dto =  await _customerDto.CreateAsync(customer.ToDto());
            return dto.ToModel();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _customerDto.GetByIdAsync(customerId);
            return customer?.ToModel();
        }

        public async Task<bool> IsValidCustomerToCreateAsync(Customer customer)
        {
            var customerDto =  await _customerDto.GetByNameAndMobileAsync(customer.Name, customer.Mobile);
            if (customerDto != null)
                return false;
            return true;
        }
    }
}
