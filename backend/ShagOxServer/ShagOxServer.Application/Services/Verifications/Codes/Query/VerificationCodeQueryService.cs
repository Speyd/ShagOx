using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Verifications.Codes.Mapping;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Application.Services.Verifications.Codes.Query;
public class VerificationCodeQueryService
    : BaseQueryService<
        VerificationCodeDto,
        VerificationCode,
        VerificationCodeSearchFilter
        >,
    IVerificationCodeQueryService
{
    public VerificationCodeQueryService(
        IVerificationCodeQueryRepository categoryQueryRepository
    )
        : base(categoryQueryRepository)
    {
    }


    protected override async Task<VerificationCodeDto> ApplyMapperAsync(
        VerificationCode entity)
    {
        return VerificationCodeMapper.ToDto(entity);
    }
}