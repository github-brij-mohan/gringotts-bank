using System;

namespace Gringotts.Services.Contracts
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public Money Amount { get; set; }
        public Money Balance { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}
