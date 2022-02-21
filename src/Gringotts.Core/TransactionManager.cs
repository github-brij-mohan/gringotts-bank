using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionManager(IAccountRepository accountRepository, ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateAsync(int customerId, int accountNumber, Transaction transactionRequest)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists");
            }

            var account = await _accountRepository.GetByIdAsync(customerId, accountNumber);
            if (account == null)
            {
                throw new Exception($"Account with account number: {accountNumber} does not exists");
            }

            ValidateAndUpdateAccountBalance(account, transactionRequest);

            //create transaction but will not save the db state
            var transaction = await _transactionRepository.CreateAsync(transactionRequest, accountNumber);

            //update account balance and save db state
            _ = await _accountRepository.UpdateAccountBalanceAsync(customerId, accountNumber, transaction.Balance.Value);
            return transaction;
        }

        private void ValidateAndUpdateAccountBalance(Account account, Transaction transaction)
        {
            if (transaction.Type.Equals(TransactionType.Deposit))
            {
                account.Balance.Value += transaction.Amount.Value;
                transaction.Balance.Value = account.Balance.Value;
            }
            else
            {
                if (account.Balance.Value >= transaction.Amount.Value)
                {
                    account.Balance.Value -= transaction.Amount.Value;
                    transaction.Balance.Value = account.Balance.Value;
                }
                else
                {
                    throw new Exception($"Transaction amount must be less than or equal to available account balance. " +
                                        $"Available account balance is {account.Balance.Value}");
                }
            }
        }

        public async Task<List<Transaction>> GetAllAsync(int customerId, int accountNumber, DateTime start, DateTime end)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with Id: {customerId} does not exists");
            }

            var account = await _accountRepository.GetByIdAsync(customerId, accountNumber);
            if (account == null)
            {
                throw new Exception($"Account with account number: {accountNumber} does not exists");
            }

            return await _transactionRepository.GetAllAsync(accountNumber, start, end);
        }

        public async Task<Transaction> GetByIdAsync(int customerId, int accountNumber, int transactionId)
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

            var transaction = await _transactionRepository.GetByIdAsync(accountNumber, transactionId);
            if (transaction == null)
            {
                throw new Exception($"Transaction with Id: {transactionId} does not exists.");
            }

            return transaction;
        }
    }
}
