using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
public interface ICategoryCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        CategoryCreateRequest request);
}