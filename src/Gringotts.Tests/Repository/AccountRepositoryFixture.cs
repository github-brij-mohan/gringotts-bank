using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.DAL.Interfaces;
using Gringotts.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository
{
    public class AccountRepositoryFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Account
            {
                CustomerId = 1,
                AccountNumber = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountDal = new Mock<IAccountDal>();
            mockAccountDal.Setup(x => x.CreateAsync(It.IsAny<DAL.Entities.Account>())).ReturnsAsync(dto);

            var repository = new AccountRepository(mockAccountDal.Object);

            var result = await repository.CreateAsync(1, account);

            result.Should().BeEquivalentTo(new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
            mockAccountDal.Verify(x => x.CreateAsync(It.IsAny<DAL.Entities.Account>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Account
            {
                CustomerId = 1,
                AccountNumber = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountDal = new Mock<IAccountDal>();
            mockAccountDal.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(dto);

            var repository = new AccountRepository(mockAccountDal.Object);

            var result = await repository.GetByIdAsync(1, 1);

            result.Should().BeEquivalentTo(new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
            mockAccountDal.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllByCustomerIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Account
            {
                CustomerId = 1,
                AccountNumber = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountDal = new Mock<IAccountDal>();
            mockAccountDal.Setup(x => x.GetAllByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(new List<DAL.Entities.Account> { dto });

            var repository = new AccountRepository(mockAccountDal.Object);

            var result = await repository.GetAllByCustomerIdAsync(1);

            result.Should().BeEquivalentTo(new List<Account> { new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 0.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            } });
            mockAccountDal.Verify(x => x.GetAllByCustomerIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Account
            {
                CustomerId = 1,
                AccountNumber = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var dto2 = new DAL.Entities.Account
            {
                CustomerId = 1,
                AccountNumber = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 1000.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var account = new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 1000.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockAccountDal = new Mock<IAccountDal>();
            mockAccountDal.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(dto);
            mockAccountDal.Setup(x => x.UpdateAsync(It.IsAny<DAL.Entities.Account>())).ReturnsAsync(dto2);

            var repository = new AccountRepository(mockAccountDal.Object);

            var result = await repository.UpdateAccountBalanceAsync(1, 1, 1000);

            result.Should().BeEquivalentTo(new Account
            {
                AccountNumber = 1,
                Type = AccountType.Current,
                Balance = new Money
                {
                    Currency = Currency.INR,
                    Value = 1000.0
                },
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
            mockAccountDal.Verify(x => x.UpdateAsync(It.IsAny<DAL.Entities.Account>()), Times.Once);
        }
    }
}
