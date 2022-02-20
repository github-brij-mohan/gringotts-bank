
namespace Gringotts.Services.Contracts
{
    public class CreateAccountRequest
    {
        public AccountType Type { get; set; }
    }

    public enum AccountType
    {
        Savings,
        Current
    }
}
