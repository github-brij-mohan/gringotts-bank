using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.Services.Contracts;
using Gringotts.Services.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service.Translators
{
    public class TransactionTranslatorFixture
    {
        [Fact]
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
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

            var result = createTransactionRequest.ToModel();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
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
            });
        }

        [Fact]
        public async Task ToResponse_WithValidRequest_ShouldReturn_ValidResponse()
        {
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

            var result = transaction.ToResponse();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
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
            });
        }
    }
}
