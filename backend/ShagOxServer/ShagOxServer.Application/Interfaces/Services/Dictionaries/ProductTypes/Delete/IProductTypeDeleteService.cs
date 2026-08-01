using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
public interface IProductTypeDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
       int id);
}