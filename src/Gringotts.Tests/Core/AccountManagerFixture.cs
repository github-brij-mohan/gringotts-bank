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
    public class AccountManagerFixture
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

            var expectedResult = new Account
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<Account>())).ReturnsAsync(expectedResult);

            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            var result = await manager.CreateAsync(1, account);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();

            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.CreateAsync(1, new Account());

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<Account>()), Times.Never);
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

            var expectedResult = new Account
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

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedResult);


            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            var result = await manager.GetByIdAsync(1, 1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();


            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1, 1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists.");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
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


            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1, 1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Account with Id: 1 does not exists.");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
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

            var expectedResult = new List<Account>
            {
                new Account
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
                }
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetAllByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);

            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            var result = await manager.GetAllAsync(1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetAllByCustomerIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithNonExistingCustomer_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();


            var manager = new AccountManager(mockAccountRepository.Object, mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.GetAllAsync(1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists.");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockAccountRepository.Verify(x => x.GetAllByCustomerIdAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
