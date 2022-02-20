using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core
{
    public class AccountManager : IAccountManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Account> CreateAsync(int customerId, Account account)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if(customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists");
            }

            return await _accountRepository.CreateAsync(customerId, account);
        }

        public async Task<List<Account>> GetAllAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists");
            }

            return await _accountRepository.GetAllByCustomerIdAsync(customerId);
        }

        public async Task<Account> GetByIdAsync(int customerId, int accountNumber)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists");
            }

            var account = await _accountRepository.GetByIdAsync(customerId, accountNumber);
            if (account == null)
            {
                throw new Exception($"Account with account number: {accountNumber} does not exists.");
            }

            return account;
        }
    }
}
