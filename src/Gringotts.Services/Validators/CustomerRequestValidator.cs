using FluentValidation;
using Gringotts.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services.Validators
{
    public class CustomerRequestValidator: AbstractValidator<CreateCustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateCustomerRequest.Name).ToLower()}");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateCustomerRequest.Address).ToLower()}");

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateCustomerRequest.Mobile).ToLower()}");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50)
                .WithMessage($"Maximum length allowed for {nameof(CreateCustomerRequest.Email).ToLower()} is 50");
        }
    }
}
