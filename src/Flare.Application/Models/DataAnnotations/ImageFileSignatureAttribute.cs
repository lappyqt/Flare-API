using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Models.DataAnnotations;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
public class ImageFileSignatureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            using var reader = new BinaryReader(file.OpenReadStream());
            var signatures = _fileSignatures.Values.SelectMany(x => x).ToList();
            var headerBytes = reader.ReadBytes(_fileSignatures.Max(x => x.Value.Max(y => y.Length)));
            bool signatureValidationResult = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));

            if (signatureValidationResult == false)
            {
                return new ValidationResult("File extension is not allowed");
            }
        }

        return ValidationResult.Success;
    }

    private readonly Dictionary<string, List<byte[]>> _fileSignatures = new Dictionary<string, List<byte[]>>
    {
        { ".jpeg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
            }
        },

        { ".png", new List<byte[]>
            {
                new byte[] { 0x89, 0x50, 0x4E, 0x47 },
                new byte[] { 0x0D, 0x0A, 0x1A, 0x0A}
            }
        },

        { ".jpg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
            }
        },

        { ".webp", new List<byte[]>
            {
                new byte[] { 0x52, 0x49, 0x46, 0x46 },
                new byte[] { 0x57, 0x45, 0x42, 0x50 }
            }
        },
    };
}