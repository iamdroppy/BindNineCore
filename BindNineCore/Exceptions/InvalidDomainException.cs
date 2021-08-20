namespace BindNineCore.Exceptions
{
    public class InvalidDomainException : DomainException
    {
        public InvalidDomainException(string domain) : base($"The given domain {domain} is invalid.")
        {
        }
    }
}