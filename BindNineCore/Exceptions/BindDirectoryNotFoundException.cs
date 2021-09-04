using System;

namespace BindNineCore.Exceptions
{
    public class BindDirectoryNotFoundException : BindNineCoreException
    {
        private const string ErrorMessage = "Bind9 directory was not found per configuration. Given value: ";
        public BindDirectoryNotFoundException(string path) : base(ErrorMessage + path)
        {
        }

        public BindDirectoryNotFoundException(string path, Exception innerException) : base(ErrorMessage + path, innerException)
        {
        }
    }
    
    public class BindDirectoryDeniedException : BindNineCoreException
    {
        private const string ErrorMessage = "Bind9 directory was denied to write/delete. Path: ";
        public BindDirectoryDeniedException(string path) : base(ErrorMessage + path)
        {
        }

        public BindDirectoryDeniedException(string path, Exception innerException) : base(ErrorMessage + path + ". Message: " + innerException.Message, innerException)
        {
        }
    }
}