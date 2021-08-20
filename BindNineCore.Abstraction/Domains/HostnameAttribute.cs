using System.ComponentModel.DataAnnotations;

namespace BindNineCore.Abstraction.Domains
{
    /// <summary>
    /// Verifies the hostname
    /// </summary>
    public class HostnameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return value is string {Length: > 0 and < 64} val
                ? !NetworkValidation.Hostname(val) ? new ValidationResult($"Invalid hostname '{val}'") : ValidationResult.Success
                : new ValidationResult("No hostname given.");
        }

    }
}