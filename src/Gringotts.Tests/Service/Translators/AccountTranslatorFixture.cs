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
    public class AccountTranslatorFixture
    {
        [Fact]
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var createAccountRequest = new CreateAccountRequest
            {
                Type = Services.Contracts.AccountType.Current,
                Currency = Services.Contracts.Currency.INR
            };

            var result = createAccountRequest.ToModel();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
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
            });
        }

        [Fact]
        public async Task ToResponse_WithValidRequest_ShouldReturn_ValidResponse()
        {
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

            var result = account.ToResponse();

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
