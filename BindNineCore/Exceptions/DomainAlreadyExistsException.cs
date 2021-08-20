namespace BindNineCore.Exceptions
{
    public class DomainAlreadyExistsException : DomainException
    {
        public DomainAlreadyExistsException(string domain) : base($"The domain {domain} already exists.")
        {
        }
    }
}