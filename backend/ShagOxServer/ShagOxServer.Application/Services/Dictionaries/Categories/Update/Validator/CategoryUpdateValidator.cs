using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
public class CategoryUpdateValidator
{
    public Result<(string name, ProductType productType)> HasChangesValidator(
       Category category,
       CategoryUpdateRequest request)
    {
        var name = request.Name ?? category.Name;
        var productType = request.ProductType ?? category.ProductType;

        if (name == category.Name &&
            productType == category.ProductType)
        {
            return Result<(string, ProductType)>.Fail("");
        }

        return Result<(string, ProductType)>.Success((name, productType));
    }
}