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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerManager _customerManager;
        private readonly IValidator<CreateCustomerRequest> _validator;
        public CustomerService(ICustomerManager customerManager, IValidator<CreateCustomerRequest> validator)
        {
            _customerManager = customerManager;
            _validator = validator;
        }
        public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest customerRequest)
        {
            var validationResult = await _validator.ValidateAsync(customerRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _customerManager.CreateAsync(customerRequest.ToModel());
            return result.ToResponse();
        }

        public async Task<CustomerResponse> GetByIdAsync(int customerId)
        {
            var result = await _customerManager.GetByIdAsync(customerId);
            return result.ToResponse();
        }
    }
}
