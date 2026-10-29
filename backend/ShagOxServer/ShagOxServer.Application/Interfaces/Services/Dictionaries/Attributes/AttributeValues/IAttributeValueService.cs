using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
public interface IAttributeValueService
{
    Task<Result<List<AttributeDto>>> GetAttributesAsync(
        JsonDocument variant,
        long categoryId,
        CancellationToken cancellationToken = default);
}
