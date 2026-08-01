using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Create;
public interface IProductTypeCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
       ProductTypeCreateRequest request);
}