using System;

namespace BindNineCore.Exceptions
{
    public abstract class SubdomainException : BindNineCoreException
    {
        protected SubdomainException(string message) : base(message)
        {
        }

        protected SubdomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}