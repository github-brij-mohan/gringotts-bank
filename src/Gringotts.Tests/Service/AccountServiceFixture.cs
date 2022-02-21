using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using Gringotts.Services;
using Gringotts.Services.Contracts;
using Gringotts.Services.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service
{
    public class AccountServiceFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var createAccountRequest = new CreateAccountRequest
            {
                Type = Services.Contracts.AccountType.Current,
                Currency = Services.Contracts.Currency.INR
            };

            var account = new Core.Models.Account
            {
                AccountNumber = 1,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var expectedResult = new AccountResponse
            {
                AccountNumber = 1,
                Type = Services.Contracts.AccountType.Current,
                Balance = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountManager = new Mock<IAccountManager>();
            mockAccountManager.Setup(x => x.CreateAsync(customerId, It.IsAny<Account>())).ReturnsAsync(account);

            var mockValidator = new Mock<IValidator<CreateAccountRequest>>();
            mockValidator.Setup(x => x.ValidateAsync(createAccountRequest, CancellationToken.None)).ReturnsAsync(new ValidationResult());

            var accountService = new AccountService(mockAccountManager.Object, mockValidator.Object);

            var result = await accountService.CreateAsync(customerId, createAccountRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CreateAsync_WithInValidRequest_ShouldThrow_ValidationException()
        {
            var customerId = 1;
            var createAccountRequest = new CreateAccountRequest
            {
                Type = null,
                Currency = Services.Contracts.Currency.INR
            };


            var mockAccountManager = new Mock<IAccountManager>();

            var accountService = new AccountService(mockAccountManager.Object, new CreateAccountRequestValidator());

            Func<Task> action = async () => await accountService.CreateAsync(customerId, createAccountRequest);

            action.Should().ThrowExactlyAsync<ValidationException>().WithMessage("Mandatory Field: type");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var accountNumber = 1;
            var account = new Core.Models.Account
            {
                AccountNumber = 1,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var expectedResult = new AccountResponse
            {
                AccountNumber = 1,
                Type = Services.Contracts.AccountType.Current,
                Balance = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountManager = new Mock<IAccountManager>();
            mockAccountManager.Setup(x => x.GetByIdAsync(customerId, accountNumber)).ReturnsAsync(account);

            var accountService = new AccountService(mockAccountManager.Object, new CreateAccountRequestValidator());

            var result = await accountService.GetByIdAsync(customerId, accountNumber);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var account1 = new Core.Models.Account
            {
                AccountNumber = 1,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account2 = new Core.Models.Account
            {
                AccountNumber = 2,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var expectedResult = new List<AccountResponse>{
                new AccountResponse
                {
                    AccountNumber = 1,
                    Type = Services.Contracts.AccountType.Current,
                    Balance = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 0.0
                    },
                    CreatedAt = DateTime.MinValue,
                    UpdatedAt = DateTime.MinValue
                },
                new AccountResponse
                {
                    AccountNumber = 2,
                    Type = Services.Contracts.AccountType.Current,
                    Balance = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 0.0
                    },
                    CreatedAt = DateTime.MinValue,
                    UpdatedAt = DateTime.MinValue
                }
            };

            var mockAccountManager = new Mock<IAccountManager>();
            mockAccountManager.Setup(x => x.GetAllAsync(customerId)).ReturnsAsync(new List<Account> { account1, account2 });

            var accountService = new AccountService(mockAccountManager.Object, new CreateAccountRequestValidator());

            var result = await accountService.GetAllAsync(customerId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
