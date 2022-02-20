using System;

namespace Gringotts.Services.Contracts
{
    public class AppException: Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
    }
}
