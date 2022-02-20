using FluentValidation;
using Gringotts.Models.Interfaces;
using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Gringotts.Services.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IValidator<CreateTransactionRequest> _validator;

        public TransactionService(ITransactionManager transactionManager, IValidator<CreateTransactionRequest> validator)
        {
            _transactionManager = transactionManager;
            _validator = validator;
        }

        public async Task<TransactionResponse> CreateAsync(int customerId, int accountNumber, CreateTransactionRequest createTransactionRequest)
        {
            var validationResult = await _validator.ValidateAsync(createTransactionRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _transactionManager.CreateAsync(customerId, accountNumber, createTransactionRequest.ToModel());
            return result.ToResponse();
        }

        public async Task<List<TransactionResponse>> GetAllAsync(int customerId, int accountNumber, DateTime start, DateTime end)
        {
            var result = await _transactionManager.GetAllAsync(customerId, accountNumber, start, end);
            return result.Select(x => x.ToResponse()).ToList();
        }

        public async Task<TransactionResponse> GetByIdAsync(int customerId, int accountNumber, int transactionId)
        {
            var result = await _transactionManager.GetByIdAsync(customerId, accountNumber, transactionId);
            return result.ToResponse();
        }
    }
}
