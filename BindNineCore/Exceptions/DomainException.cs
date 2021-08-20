using System;

namespace BindNineCore.Exceptions
{
    public class DomainException : BindNineCoreException
    {
        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}