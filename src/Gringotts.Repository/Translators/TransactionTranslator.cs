using System;
using Model = Gringotts.Core.Models;
using Dal = Gringotts.DAL.Entities;
using Newtonsoft.Json;

namespace Gringotts.Repository.Translators
{
    internal static class TransactionTranslator
    {
        internal static Dal.Transaction ToDto(this Model.Transaction transaction, int accountNumber)
        {
            return new Dal.Transaction
            {
                AccountNumber = accountNumber,
                Amount = transaction.Amount.Value,
                Currency = transaction.Amount.Currency.ToString(),
                Balance = JsonConvert.SerializeObject(transaction.Balance),
                Type = transaction.Type.ToString(),
                Description = transaction.Description
            };
        }

        internal static Model.Transaction ToModel(this Dal.Transaction transaction)
        {
            var balance = JsonConvert.DeserializeObject<Model.Money>(transaction?.Balance);
            return new Model.Transaction
            {
                Id = transaction.Id,
                Amount = new Model.Money
                {
                    Currency = Model.Currency.INR,
                    Value = transaction.Amount
                },
                Balance = balance,
                Type = transaction.Type.Equals(Model.TransactionType.Deposit.ToString(), System.StringComparison.OrdinalIgnoreCase) ? Model.TransactionType.Deposit : Model.TransactionType.Withdraw,
                Description = transaction.Description,
                Time = transaction.Time
            };
        }
    }
}
