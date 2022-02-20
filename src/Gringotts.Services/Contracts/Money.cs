namespace Gringotts.Services.Contracts
{
    public class Money
    {
        public Currency Currency { get; set; }
        public double Value { get; set; }
    }

    public enum Currency
    {
        INR
    }
}