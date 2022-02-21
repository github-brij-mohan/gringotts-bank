using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.DAL.Interfaces;
using Gringotts.Repository;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository
{
    public class TransactionRepositoryFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Transaction
            {
                Id = 1,
                AccountNumber = 1,
                Amount = 100.0,
                Currency = "INR",
                Balance = JsonConvert.SerializeObject(new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                }),
                Type = "Deposit",
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var transaction = new Transaction
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

            var mockTransactionDal = new Mock<ITransactionDal>();
            mockTransactionDal.Setup(x => x.CreateAsync(It.IsAny<DAL.Entities.Transaction>())).ReturnsAsync(dto);

            var repository = new TransactionRepository(mockTransactionDal.Object);

            var result = await repository.CreateAsync(transaction, 1);

            result.Should().BeEquivalentTo(transaction);
            mockTransactionDal.Verify(x => x.CreateAsync(It.IsAny<DAL.Entities.Transaction>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Transaction
            {
                Id = 1,
                AccountNumber = 1,
                Amount = 100.0,
                Currency = "INR",
                Balance = JsonConvert.SerializeObject(new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                }),
                Type = "Deposit",
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var transaction = new Transaction
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

            var mockTransactionDal = new Mock<ITransactionDal>();
            mockTransactionDal.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(dto);

            var repository = new TransactionRepository(mockTransactionDal.Object);

            var result = await repository.GetByIdAsync(1, 1);

            result.Should().BeEquivalentTo(transaction);
            mockTransactionDal.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Transaction
            {
                Id = 1,
                AccountNumber = 1,
                Amount = 100.0,
                Currency = "INR",
                Balance = JsonConvert.SerializeObject(new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0
                }),
                Type = "Deposit",
                Description = "test transaction",
                Time = DateTime.MinValue
            };

            var transaction = new Transaction
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

            var mockTransactionDal = new Mock<ITransactionDal>();
            mockTransactionDal.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(new List<DAL.Entities.Transaction> { dto });

            var repository = new TransactionRepository(mockTransactionDal.Object);

            var result = await repository.GetAllAsync(1, DateTime.MinValue, DateTime.Now);

            result.Should().BeEquivalentTo(new List<Transaction> { transaction });
            mockTransactionDal.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
