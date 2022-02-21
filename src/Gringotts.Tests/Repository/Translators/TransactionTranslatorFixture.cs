using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.Repository.Translators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository.Translators
{
    public class TransactionTranslatorFixture
    {
        [Fact]
        public async Task ToDto_WithValidRequest_ShouldReturn_ValidResponse()
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

            var result = transaction.ToDto(1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
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
            });
        }

        [Fact]
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var transaction = new DAL.Entities.Transaction
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

            var result = transaction.ToModel();

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
