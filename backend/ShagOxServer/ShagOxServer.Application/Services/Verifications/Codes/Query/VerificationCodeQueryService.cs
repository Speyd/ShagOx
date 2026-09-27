using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Services.Verifications.Codes.Mapping;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Verifications.Codes.Query;
public class VerificationCodeQueryService
    : IVerificationCodeQueryService
{
    private readonly IVerificationCodeQueryRepository _categoryQueryRepository;


    public VerificationCodeQueryService(
        IVerificationCodeQueryRepository categoryQueryRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
    }


    public async Task<Result<VerificationCodeDto>> GetByIdAsync(
        long id)
    {
        var category = await _categoryQueryRepository
            .GetByIdAsync(id);

        return category.ToResult(VerificationCodeMapper.ToDto);
    }

    public async Task<Result<PagedResult<VerificationCodeDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .GetPagedAsync(pagination);

        return categories.ToResultPaged(VerificationCodeMapper.ToDto);
    }

    public async Task<Result<PagedResult<VerificationCodeDto>>> Search(
        VerificationCodeSearchFilter filter,
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .SearchAsync(filter, pagination);

        return categories.ToResultPaged(VerificationCodeMapper.ToDto);
    }
}