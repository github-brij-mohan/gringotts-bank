using System;
using System.Runtime.CompilerServices;
using Contract = Gringotts.Services.Contracts;
using Model = Gringotts.Core.Models;
[assembly: InternalsVisibleTo("Gringotts.Tests")]

namespace Gringotts.Services.Translators
{
    internal static class TransactionTranslator
    {
        internal static Model.Transaction ToModel(this Contracts.CreateTransactionRequest transactionRequest)
        {
            return new Model.Transaction
            {
                Amount = new Model.Money
                {
                    Currency = Model.Currency.INR,
                    Value = transactionRequest.Amount.Value
                },
                Balance = new Model.Money
                {
                    Currency = Model.Currency.INR
                },
                Type = transactionRequest.Type.ToString().Equals(Contract.TransactionType.Deposit.ToString(), StringComparison.OrdinalIgnoreCase) ? Model.TransactionType.Deposit : Model.TransactionType.Withdraw,
                Description = transactionRequest.Description
            };
        }

        internal static Contract.TransactionResponse ToResponse(this Model.Transaction transaction)
        {
            return new Contract.TransactionResponse
            {
                Id = transaction.Id,
                Amount = new Contract.Money
                {
                    Currency = Contract.Currency.INR,
                    Value = transaction.Amount.Value
                },
                Balance = new Contract.Money
                {
                    Currency = Contract.Currency.INR,
                    Value = transaction.Balance.Value
                },
                Type = transaction.Type.ToString().Equals(Contract.TransactionType.Deposit.ToString(), StringComparison.OrdinalIgnoreCase) ? Contract.TransactionType.Deposit : Contract.TransactionType.Withdraw,
                Description = transaction.Description,
                Time = transaction.Time
            };
        }
    }
}
