using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
public interface IProductTypeDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<ProductType, DeleteResponse>
{
}
