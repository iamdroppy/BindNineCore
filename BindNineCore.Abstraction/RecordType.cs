using System;

namespace BindNineCore.Abstraction
{
    /// <summary>
    /// The type of DNS record
    /// </summary>
    public enum RecordType 
    {
        /// <summary>
        /// An ALIAS (A) is meant to point a hostname ({subdomain}.{domain}) to a certain
        /// IP address.
        /// </summary>
        Alias, // A
        Nameserver, // NS
        CNAME,
        Text
    }

    public static class RecordTypeExtensions
    {
        public static string GetShortName(this RecordType type) =>
            type switch
            {
                RecordType.Alias => "A",
                RecordType.Nameserver => "NS",
                RecordType.CNAME => "CNAME",
                RecordType.Text => "TXT",
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
    }
}