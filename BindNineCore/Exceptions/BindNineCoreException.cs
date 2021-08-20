using System;

namespace BindNineCore.Exceptions
{
    /// <summary>
    /// BindNine Core exception
    /// </summary>
    public abstract class BindNineCoreException : Exception
    {
        public BindNineCoreException(string message) : base(message) {}
        public BindNineCoreException(string message, Exception innerException) : base(message, innerException) {}
    }
}