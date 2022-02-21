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
    public class CreateTransactionRequestValidatorFixture
    {
        [Theory]
        [InlineData(TransactionType.Deposit)]
        [InlineData(TransactionType.Withdraw)]
        public async Task ValidateAsync_WithValidInput_ShouldReturn_True(TransactionType transactionType)
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Money
                {
                    Currency = Currency.INR,
                    Value = 100
                },
                Type = transactionType,
                Description = "for testing"
            };

            var validator = new CreateTransactionRequestValidator();

            var result = await validator.ValidateAsync(createTransactionRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidateAsync_WithMissingAmount_ShouldReturn_False()
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = null,
                Type = TransactionType.Deposit,
                Description = "for testing"
            };

            var validator = new CreateTransactionRequestValidator();

            var result = await validator.ValidateAsync(createTransactionRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: amount");
        }

        [Fact]
        public async Task ValidateAsync_WithAmountZero_ShouldReturn_False()
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Money
                {
                    Currency = Currency.INR,
                    Value = 0
                },
                Type = TransactionType.Deposit,
                Description = "for testing"
            };

            var validator = new CreateTransactionRequestValidator();

            var result = await validator.ValidateAsync(createTransactionRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Transaction amount should be greater than 0");
        }

        [Fact]
        public async Task ValidateAsync_WithMissingType_ShouldReturn_False()
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Money
                {
                    Currency = Currency.INR,
                    Value = 100
                },
                Type = null,
                Description = "for testing"
            };

            var validator = new CreateTransactionRequestValidator();

            var result = await validator.ValidateAsync(createTransactionRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: type");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ValidateAsync_WithMissingDescription_ShouldReturn_False(string description)
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Money
                {
                    Currency = Currency.INR,
                    Value = 100
                },
                Type = TransactionType.Withdraw,
                Description = description
            };

            var validator = new CreateTransactionRequestValidator();

            var result = await validator.ValidateAsync(createTransactionRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: description");
        }
    }
}
