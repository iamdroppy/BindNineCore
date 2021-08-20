using System.ComponentModel.DataAnnotations;

namespace BindNineCore.Abstraction.Dns
{
    public class SubdomainAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return value is string {Length: > 0 and < 64} val
                ? !NetworkValidation.Subdomain(val) ? new ValidationResult($"Invalid subdomain '{val}'") : ValidationResult.Success
                : new ValidationResult("No subdomain given.");
        }

    }
    
    public class IpAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return value is string {Length: > 0 and < 64} val
                ? !NetworkValidation.IpAddress(val) ? new ValidationResult($"Invalid subdomain '{val}'") : ValidationResult.Success
                : new ValidationResult("No subdomain given.");
        }

    }
}