using Model = Gringotts.Core.Models;
using Dal = Gringotts.DAL.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Gringotts.Tests")]

namespace Gringotts.Repository.Translators
{
    internal static class AccountTranslator
    {
        internal static Dal.Account ToDto(this Model.Account account, int customerId)
        {
            return new Dal.Account
            {
                CustomerId = customerId,
                Type = account.Type.ToString(),
                Currency = account.Balance.Currency.ToString(),
                Balance = account.Balance.Value
            };
        }

        internal static Model.Account ToModel(this Dal.Account account)
        {
            return new Model.Account
            {
                AccountNumber = account.AccountNumber,
                Type = account.Type.Equals(Model.AccountType.Savings.ToString(), System.StringComparison.OrdinalIgnoreCase) ? Model.AccountType.Savings : Model.AccountType.Current,
                Balance = new Model.Money
                {
                    Currency = Model.Currency.INR,
                    Value = account.Balance
                },
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };
        }
    }
}
