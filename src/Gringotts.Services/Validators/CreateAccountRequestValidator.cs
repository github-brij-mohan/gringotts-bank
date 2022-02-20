using FluentValidation;
using Gringotts.Services.Contracts;

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

            RuleFor(x => x.Currency)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Mandatory Field: {nameof(CreateAccountRequest.Currency).ToLower()}");
        }
    }
}
