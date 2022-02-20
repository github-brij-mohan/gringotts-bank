namespace Gringotts.Services.Contracts
{
    public class CreateTransactionRequest
    {
        public Money Amount { get; set; }
        public TransactionType? Type { get; set; }
        public string Description { get; set; }
    }

    public enum TransactionType
    {
        Withdraw,
	    Deposit
    }
}
