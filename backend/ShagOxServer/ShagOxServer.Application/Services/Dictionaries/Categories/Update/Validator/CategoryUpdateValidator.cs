using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Runtime.CompilerServices;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
public class CategoryUpdateValidator
{
    public Result<(string name, long productTypeId)> HasChangesValidator(
       Category category,
       CategoryUpdateRequest request)
    {
        var code = request.Code ?? category.Code;
        var productTypeId = request.ProductTypeId ?? category.ProductTypeId;

        if (code == category.Code &&
            productTypeId == category.ProductTypeId)
        {
            return Result<(string, long)>.Fail(null);
        }

        return Result<(string, long)>.Success((code, productTypeId));
    }
}