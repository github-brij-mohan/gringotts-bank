namespace Gringotts.Core.Models
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