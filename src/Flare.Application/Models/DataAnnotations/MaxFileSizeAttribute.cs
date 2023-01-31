using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Models.DataAnnotations;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file && file.Length > _maxFileSize)
        {
            return new ValidationResult($"Maximum allowed file size is { _maxFileSize} bytes.");
        }

        return ValidationResult.Success;
    }
}