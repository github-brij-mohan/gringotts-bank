using FluentAssertions;
using Gringotts.Core;
using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests
{
    public class TransactionManagerFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidReuest_ShouldReturn_ValidResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
            {
                AccountNumber = 0,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var transaction = new Core.Models.Transaction
            {
                Id = 0,
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

            var expectedResult = new Core.Models.Transaction
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(x => x.CreateAsync(It.IsAny<Core.Models.Transaction>(), It.IsAny<int>())).ReturnsAsync(expectedResult);

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            var result = await manager.CreateAsync(1, 1, transaction);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.CreateAsync(1, 1, new Transaction());

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WithNonExistingAccount_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.CreateAsync(1, 1, new Transaction());

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Account with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WithTransactionAmountGreaterThanAvailableBalance_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
            {
                AccountNumber = 0,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 1000.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var transaction = new Core.Models.Transaction
            {
                Id = 0,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 2000
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.CreateAsync(1, 1, new Transaction());

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Transaction amount must be less than or equal to available account balance. " +
                                                                       "Available account balance is 2000.0");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidReuest_ShouldReturn_ValidResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
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

            var transaction = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 2000
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(transaction);

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            var result = await manager.GetByIdAsync(1, 1, 1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(transaction);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1, 1, 1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingAccount_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1, 1, 1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Account with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingTransaction_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
            {
                AccountNumber = 0,
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 1000.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1, 1, 1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Transaction with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_WithValidReuest_ShouldReturn_ValidResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
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

            var transaction = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 2000
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(new List<Transaction> { transaction });

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            var result = await manager.GetAllAsync(1, 1, DateTime.MinValue, DateTime.Now);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new List<Transaction> { transaction });
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithNoTransactionInTimeRange_ShouldReturn_EmptyResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
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

            var transaction = new Core.Models.Transaction
            {
                Id = 1,
                Amount = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 2000
                },
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                },
                Type = Core.Models.TransactionType.Deposit,
                Description = "test transaction",
                Time = DateTime.Now.AddDays(-2)
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(account);

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(new List<Transaction> {  });

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            var result = await manager.GetAllAsync(1, 1, DateTime.Now.AddDays(-1), DateTime.Now);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.GetAllAsync(1, 1, DateTime.MinValue, DateTime.Now);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            mockTransactionRepository.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_WithNonExistingAccount_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            var manager = new TransactionManager(mockAccountRepository.Object, mockCustomerRepository.Object, mockTransactionRepository.Object);

            Func<Task> result = async () => await manager.GetAllAsync(1, 1, DateTime.MinValue, DateTime.Now);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Account with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionRepository.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
        }
    }
}
