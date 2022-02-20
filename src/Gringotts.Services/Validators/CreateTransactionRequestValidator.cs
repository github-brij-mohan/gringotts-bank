using FluentValidation;
using Gringotts.Services.Contracts;

namespace Gringotts.Services.Validators
{
    public class CreateTransactionRequestValidator: AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionRequestValidator()
        {
            RuleFor(x => x.Amount)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage($"Mandatory Field: {nameof(CreateTransactionRequest.Amount).ToLower()}")
               .ChildRules(y =>
               {
                   y.RuleFor(z => z.Value)
                   .Cascade(CascadeMode.Stop)
                   .GreaterThan(0)
                   .WithMessage("Transaction amount should be greater than 0");
               });

            RuleFor(x => x.Type)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage($"Mandatory Field: {nameof(CreateTransactionRequest.Type).ToLower()}");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateTransactionRequest.Description).ToLower()}");
        }
    }
}
