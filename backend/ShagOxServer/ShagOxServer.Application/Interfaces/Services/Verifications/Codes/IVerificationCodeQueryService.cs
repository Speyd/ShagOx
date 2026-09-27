using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
public interface IVerificationCodeQueryService
    : IQueryService<VerificationCodeDto, 
        VerificationCodeSearchFilter>
{
}