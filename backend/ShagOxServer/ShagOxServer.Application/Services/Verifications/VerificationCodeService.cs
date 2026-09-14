using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Application.Services.Verifications.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using System.Security.Cryptography;

namespace ShagOxServer.Application.Services.Verifications;

public class VerificationCodeService
    : IVerificationCodeService
{
    private readonly IRepository<VerificationCode> _repository;
    private readonly IVerificationCodeQueryRepository _queryRepository;

    private readonly IUnitOfWork _unitOfWork;


    public VerificationCodeService(
        IRepository<VerificationCode> repository,
        IVerificationCodeQueryRepository queryRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> CreateCodeAsync(int userId)
    {
        var oldCode = await _queryRepository
            .GetActiveByUserIdAsync(userId);

        if (oldCode is not null)
            oldCode.InvalidatedAt = DateTime.UtcNow;

        var code = RandomNumberGenerator
            .GetInt32(100000, 1000000)
            .ToString();

        var codeHash = BCrypt.Net.BCrypt.HashPassword(code);

        var verificationCode = new VerificationCode
        {
            UserId = userId,
            CodeHash = codeHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        };

        _repository.Add(verificationCode);

        return code;
    }

    public async Task<VerificationCodeResult> VerifyCodeAsync(
    int userId,
    string code)
    {
        var verificationCode =
            await _queryRepository.GetActiveByUserIdAsync(userId);

        if (verificationCode is null)
            return VerificationCodeResult.NotFound;

        if (verificationCode.UsedAt is not null)
            return VerificationCodeResult.AlreadyUsed;

        if (verificationCode.ExpiresAt < DateTime.UtcNow)
            return VerificationCodeResult.Expired;

        if (verificationCode.Attempts >= 3)
        {
            verificationCode.InvalidatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return VerificationCodeResult.AttemptsExceeded;
        }

        var valid = BCrypt.Net.BCrypt.Verify(
            code,
            verificationCode.CodeHash);

        if (!valid)
        {
            verificationCode.Attempts++;

            await _unitOfWork.SaveChangesAsync();

            return VerificationCodeResult.Invalid;
        }

        verificationCode.UsedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        return VerificationCodeResult.Success;
    }
}