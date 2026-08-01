using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
public interface ICategoryDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}