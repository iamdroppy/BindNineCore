namespace BindNineCore.Exceptions
{
    public class SubdomainAlreadyExistsException : SubdomainException
    {
        public string Subdomain { get; }
        public string Domain { get; }

        public SubdomainAlreadyExistsException(string subdomain) : base($"The subdomain {subdomain} already exists.")
        {
            Subdomain = subdomain;
        }
        
        public SubdomainAlreadyExistsException(string subdomain, string domain) : base($"The subdomain {subdomain} already exists on domain {domain}.")
        {
            Subdomain = subdomain;
            Domain = domain;
        }
    }
}