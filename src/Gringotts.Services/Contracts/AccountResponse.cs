using System;

namespace Gringotts.Services.Contracts
{
    public class AccountResponse
    {
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public Money Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
