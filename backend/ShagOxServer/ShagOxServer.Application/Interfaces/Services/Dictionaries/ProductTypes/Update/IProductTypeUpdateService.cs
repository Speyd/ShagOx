using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
public interface IProductTypeUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int productTypeId,
        ProductTypeUpdateRequest request);
}