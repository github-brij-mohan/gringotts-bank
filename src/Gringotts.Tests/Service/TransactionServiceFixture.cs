using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Gringotts.Core;
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
    public class TransactionServiceFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var accountNumber = 1;
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 100
                },
                Type = Services.Contracts.TransactionType.Deposit,
                Description = "test transaction"
            };

            var transaction = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 100
                },
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                },
                Type = Core.Models.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var expectedResult = new TransactionResponse
            {
                Id = 1,
                Amount = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 100
                },
                Balance = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 0
                },
                Type = Services.Contracts.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var mockTransactionManager = new Mock<ITransactionManager>();
            mockTransactionManager.Setup(x => x.CreateAsync(customerId, accountNumber, It.IsAny<Transaction>())).ReturnsAsync(transaction);

            var mockValidator = new Mock<IValidator<CreateTransactionRequest>>();
            mockValidator.Setup(x => x.ValidateAsync(createTransactionRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

            var transactionService = new TransactionService(mockTransactionManager.Object, mockValidator.Object);

            var result = await transactionService.CreateAsync(customerId, accountNumber, createTransactionRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CreateAsync_WithInValidRequest_ShouldThrow_ValidationException()
        {
            var customerId = 1;
            var accountNumber = 1;
            var createTransactionRequest = new CreateTransactionRequest
            {
                Amount = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 100
                },
                Type = null,
                Description = "test transaction"
            };

            var mockTransactionManager = new Mock<ITransactionManager>();

            var transactionService = new TransactionService(mockTransactionManager.Object, new CreateTransactionRequestValidator());

            Func<Task> action = async () => await transactionService.CreateAsync(customerId, accountNumber, createTransactionRequest);

            action.Should().ThrowExactlyAsync<ValidationException>().WithMessage("Mandatory Field: type");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var accountNumber = 1;
            var transactionId = 1;

            var transaction = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 100
                },
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                },
                Type = Core.Models.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var expectedResult = new TransactionResponse
            {
                Id = 1,
                Amount = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 100
                },
                Balance = new Services.Contracts.Money
                {
                    Currency = Services.Contracts.Currency.INR,
                    Value = 0
                },
                Type = Services.Contracts.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var mockTransactionManager = new Mock<ITransactionManager>();
            mockTransactionManager.Setup(x => x.GetByIdAsync(customerId, accountNumber, transactionId)).ReturnsAsync(transaction);

            var transactionService = new TransactionService(mockTransactionManager.Object, new CreateTransactionRequestValidator());

            var result = await transactionService.GetByIdAsync(customerId, accountNumber, transactionId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
            var accountNumber = 1;

            var transaction1 = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 100
                },
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                },
                Type = Core.Models.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var transaction2 = new Core.Models.Transaction
            {
                Id = 2,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 100
                },
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                },
                Type = Core.Models.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var expectedResult = new List<TransactionResponse>
            {
                new TransactionResponse
                {
                    Id = 1,
                    Amount = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 100
                    },
                    Balance = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 0
                    },
                    Type = Services.Contracts.TransactionType.Deposit,
                    Description = "test transaction",
                    Time = DateTime.MinValue
                },
                new TransactionResponse
                {
                    Id = 2,
                    Amount = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 100
                    },
                    Balance = new Services.Contracts.Money
                    {
                        Currency = Services.Contracts.Currency.INR,
                        Value = 0
                    },
                    Type = Services.Contracts.TransactionType.Deposit,
                    Description = "test transaction",
                    Time = DateTime.MinValue
                }
            };

            var mockTransactionManager = new Mock<ITransactionManager>();
            mockTransactionManager.Setup(x => x.GetAllAsync(customerId, accountNumber, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(new List<Transaction> { transaction1, transaction2 });

            var transactionService = new TransactionService(mockTransactionManager.Object, new CreateTransactionRequestValidator());

            var result = await transactionService.GetAllAsync(customerId, accountNumber, DateTime.MinValue, DateTime.Now);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
