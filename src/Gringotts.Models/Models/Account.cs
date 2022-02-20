using System;

namespace Gringotts.Core.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public Money Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum AccountType
    {
        Savings,
        Current
    }
}
