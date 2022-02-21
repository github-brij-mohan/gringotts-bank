using System;
using System.Runtime.CompilerServices;
using Contract = Gringotts.Services.Contracts;
using Model = Gringotts.Core.Models;
[assembly: InternalsVisibleTo("Gringotts.Tests")]

namespace Gringotts.Services.Translators
{
    internal static class AccountTranslator
    {
        internal static Model.Account ToModel(this Contracts.CreateAccountRequest accountRequest)
        {
            return new Model.Account
            {
                Type = accountRequest.Type.ToString().Equals(Contract.AccountType.Savings.ToString(), StringComparison.OrdinalIgnoreCase) ? Model.AccountType.Savings : Model.AccountType.Current,
                Balance = new Model.Money
                {
                    Currency = Model.Currency.INR,
                    Value = 0.0
                }
            };
        }

        internal static Contract.AccountResponse ToResponse(this Model.Account account)
        {
            return new Contract.AccountResponse
            {
                AccountNumber = account.AccountNumber,
                Type = account.Type.ToString().Equals(Model.AccountType.Savings.ToString(), StringComparison.OrdinalIgnoreCase) ? Contract.AccountType.Savings : Contract.AccountType.Current,
                Balance = new Contract.Money
                {
                    Currency = Contract.Currency.INR,
                    Value = account.Balance.Value
                },
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };
        }
    }
}
