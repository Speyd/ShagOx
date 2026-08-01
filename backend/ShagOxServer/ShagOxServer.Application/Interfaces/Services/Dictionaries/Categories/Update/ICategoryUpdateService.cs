using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
public interface ICategoryUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int categoryId,
        CategoryUpdateRequest request);
}