using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update.Validator;
public class LineItemAttributeUpdateValidator
{
    public Result<(int order, long attributeDefinitionId)> HasChangesValidator(
       LineItemAttribute attribute,
       LineItemAttributeUpdateRequest request)
    {
        var order = request.Order ?? attribute.Order;

        var attributeDefinitionId = request.AttributeDefinitionId 
            ?? attribute.AttributeDefinitionId;


        if (order == attribute.Order &&
            attributeDefinitionId == attribute.AttributeDefinitionId)
        {
            return Result<(int, long)>.Fail("");
        }

        return Result<(int, long)>.Success((order, attributeDefinitionId));
    }
}
