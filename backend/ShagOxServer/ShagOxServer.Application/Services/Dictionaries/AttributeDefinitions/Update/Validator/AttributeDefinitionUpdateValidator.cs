using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update.Validator;
public class AttributeDefinitionUpdateValidator
{
    public Result<(string key, int categoryId)> HasChangesValidator(
       AttributeDefinition attribute,
       AttributeDefinitionUpdateRequest request)
    {
        var key = request.Key ?? attribute.Key;
        var categoryId = request.CategoryId ?? attribute.CategoryId;

        if (key == attribute.Key &&
            categoryId == attribute.CategoryId)
        {
            return Result<(string, int)>.Fail("");
        }

        return Result<(string, int)>.Success((key, categoryId));
    }
}