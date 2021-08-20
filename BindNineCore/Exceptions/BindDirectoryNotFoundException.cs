using System;

namespace BindNineCore.Exceptions
{
    public class BindDirectoryNotFoundException : BindNineCoreException
    {
        private const string ErrorMessage = "Bind9 directory was not found per configuration. Given value: ";
        public BindDirectoryNotFoundException(string url) : base(ErrorMessage + url)
        {
        }

        public BindDirectoryNotFoundException(string url, Exception innerException) : base(ErrorMessage + url, innerException)
        {
        }
    }
}