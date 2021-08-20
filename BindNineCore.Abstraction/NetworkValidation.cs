using System.Net;
using System.Text.RegularExpressions;

namespace BindNineCore.Abstraction
{
    public static class NetworkValidation
    {
        public static bool Hostname(string obj) => obj != null && Regex.IsMatch(obj, @"^[a-zA-Z_\-][a-zA-Z0-9_\-]*(\.[a-zA-Z_\-][a-zA-Z0-9_\-]+?)?\.[a-zA-Z]+$");
        public static bool Subdomain(string obj) => obj != null && Regex.IsMatch(obj, @"^(@|[a-zA-Z_\-][a-zA-Z0-9_\-]*?)(\.[a-zA-Z_\-][a-zA-Z0-9_\-]*?)*?$");
        public static bool IpAddress(string obj) => obj != null && IPAddress.TryParse(obj, out _);
    }
}