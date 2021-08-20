namespace BindNineCore.Exceptions
{
    public class InvalidSubdomainException : SubdomainException
    {
        public string Subdomain { get; }

        public InvalidSubdomainException(string subdomain) : base($"The subdomain {subdomain} is invalid.")
        {
            Subdomain = subdomain;
        }
    }
}