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
    public class AccountService : IAccountService
    {
        private readonly IAccountManager _accountManager;
        private readonly IValidator<CreateAccountRequest> _validator;

        public AccountService(IAccountManager accountManager, IValidator<CreateAccountRequest> validator)
        {
            _accountManager = accountManager;
            _validator = validator;
        }

        public async Task<AccountResponse> CreateAsync(int customerId, CreateAccountRequest createAccountRequest)
        {
            var validationResult = await _validator.ValidateAsync(createAccountRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _accountManager.CreateAsync(customerId, createAccountRequest.ToModel());
            return result.ToResponse();
        }

        public async Task<List<AccountResponse>> GetAllAsync(int customerId)
        {
            var result = await _accountManager.GetAllAsync(customerId);
            return result.Select(x => x.ToResponse()).ToList();
        }

        public async Task<AccountResponse> GetByIdAsync(int customerId, int accountNumber)
        {
            var result = await _accountManager.GetByIdAsync(customerId, accountNumber);
            return result.ToResponse();
        }
    }
}
