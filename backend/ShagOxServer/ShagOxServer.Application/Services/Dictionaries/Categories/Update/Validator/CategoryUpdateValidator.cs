using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
public class CategoryUpdateValidator
{
    public Result<(string name, int productTypeId)> HasChangesValidator(
       Category category,
       CategoryUpdateRequest request)
    {
        var name = request.Name ?? category.Name;
        var productTypeId = request.ProductTypeId ?? category.ProductTypeId;

        if (name == category.Name &&
            productTypeId == category.ProductTypeId)
        {
            return Result<(string, int)>.Fail("");
        }

        return Result<(string, int)>.Success((name, productTypeId));
    }
}