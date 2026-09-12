using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Update.Validator;
public class BasketAttributeUpdateValidator
{
    public Result<(int order, int attributeDefinitionId)> HasChangesValidator(
       BasketAttribute attribute,
       BasketAttributeUpdateRequest request)
    {
        var order = request.Order ?? attribute.Order;

        var attributeDefinitionId = request.AttributeDefinitionId 
            ?? attribute.AttributeDefinitionId;


        if (order == attribute.Order &&
            attributeDefinitionId == attribute.AttributeDefinitionId)
        {
            return Result<(int, int)>.Fail("");
        }

        return Result<(int, int)>.Success((order, attributeDefinitionId));
    }
}