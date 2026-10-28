using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
public interface IAttributeProcessor
{
    Task<Result<List<AttributeDto>>> GetAttributesAsync(
        JsonDocument variant,
        long categoryId,
        CancellationToken cancellationToken = default);
}
