using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.Repository.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository.Translators
{
    public class AccountTranslatorFixture
    {
        [Fact]
        public async Task ToDto_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var createAccountRequest = new Account
            {
                Type = Core.Models.AccountType.Current,
                Balance = new Core.Models.Money
                {
                    Currency = Core.Models.Currency.INR,
                    Value = 0.0
                }
            };

            var result = createAccountRequest.ToDto(1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                CustomerId = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
        }

        [Fact]
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var account = new DAL.Entities.Account
            {
                AccountNumber = 1,
                CustomerId = 1,
                Type = "Current",
                Currency = "INR",
                Balance = 0.0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var result = account.ToModel();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
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
            });
        }
    }
}
