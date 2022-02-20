using FluentAssertions;
using Gringotts.Services.Contracts;
using Gringotts.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service.Validators
{
    public class CreateAccountValidatorFixture
    {
        [Theory]
        [InlineData(AccountType.Savings, Currency.INR)]
        [InlineData(AccountType.Current, Currency.INR)]
        public async Task ValidateAsync_WithValidInput_ShouldReturn_True(AccountType accountType, Currency currency)
        {
            var createAccountRequest = new CreateAccountRequest
            {
                Type = accountType,
                Currency = currency
            };

            var validator = new CreateAccountRequestValidator();

            var result = await validator.ValidateAsync(createAccountRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidateAsync_WithMissingAccountType_ShouldReturn_False()
        {
            var createAccountRequest = new CreateAccountRequest
            {
                Type = null,
                Currency = Currency.INR
            };

            var validator = new CreateAccountRequestValidator();

            var result = await validator.ValidateAsync(createAccountRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: type");
        }

        [Fact]
        public async Task ValidateAsync_WithMissingCurrency_ShouldReturn_False()
        {
            var createAccountRequest = new CreateAccountRequest
            {
                Type = AccountType.Savings,
                Currency = null
            };

            var validator = new CreateAccountRequestValidator();

            var result = await validator.ValidateAsync(createAccountRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: currency");
        }
    }
}
