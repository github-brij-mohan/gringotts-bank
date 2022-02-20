using FluentValidation;
using Gringotts.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Services.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(x => x.Type)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateAccountRequest.Type).ToLower()}");
        }
    }
}
