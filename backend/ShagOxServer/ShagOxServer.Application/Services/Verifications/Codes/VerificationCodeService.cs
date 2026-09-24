using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Verifivations;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Application.Resources.Verifications.Codes;
using ShagOxServer.Application.Services.Verifications.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Security.Cryptography;

namespace ShagOxServer.Application.Services.Verifications.Codes;
public class VerificationCodeService
    : IVerificationCodeService
{
    private readonly IRepository<VerificationCode> _repository;
    private readonly IVerificationCodeQueryRepository _queryRepository;
    private readonly VerificationCodeSettings _options;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<VerificationCodeService> _logger;


    public VerificationCodeService(
        IRepository<VerificationCode> repository,
        IVerificationCodeQueryRepository queryRepository,
        IOptions<VerificationCodeSettings> options,
        IUnitOfWork unitOfWork,
        ILogger<VerificationCodeService> logger)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _options = options.Value;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> CreateCodeAsync(
        int userId,
        VerificationCodePurpose purpose,
        string? pendingValue = null)
    {
        var oldCode = await _queryRepository
            .GetActiveByUserIdAsync(userId);

        if (oldCode is not null)
            oldCode.InvalidatedAt = DateTime.UtcNow;

        var code = RandomNumberGenerator
            .GetInt32(_options.MinGenValue, _options.MaxGenValue)
            .ToString();

        var codeHash = BCrypt.Net.BCrypt.HashPassword(code);

        var verificationCode = new VerificationCode
        {
            UserId = userId,
            CodeHash = codeHash,
            ExpiresAt = DateTime.UtcNow
                .AddMinutes(_options.ExpirationMinutes),
            Purpose = purpose,
            PendingValue = pendingValue
        };

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(verificationCode);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create verification code.UserId: {UserId}",
               userId);

            return Result<string>
                .Fail(VerificationCodeResources.FailedToCreateVerificationCode);
        }

        _logger.LogInformation(
            "Verification code created successfully. Id: {Id}",
            verificationCode.Id);

        return Result<string>.Success(code);
    }

    public async Task<VerificationResult> VerifyCodeAsync(
        int userId,
        string hashCode)
    {
        var code =
            await _queryRepository.GetActiveByUserIdAsync(userId);

        if (code is null)
            return new (VerificationCodeResult.NotFound, code);

        if (code.UsedAt is not null)
            return new(VerificationCodeResult.AlreadyUsed, code);

        if (code.ExpiresAt < DateTime.UtcNow)
            return new(VerificationCodeResult.Expired, code);

        if (code.Attempts >= _options.MaxAttempts)
        {
            code.InvalidatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return new(VerificationCodeResult.AttemptsExceeded, code);
        }

        var valid = BCrypt.Net.BCrypt.Verify(
            hashCode,
            code.CodeHash);

        if (!valid)
        {
            code.Attempts++;

            await _unitOfWork.SaveChangesAsync();

            return new(VerificationCodeResult.Invalid, code);
        }

        code.UsedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation(
            "Verification code verified successfully. UserId: {UserId}",
            userId);

        return new(VerificationCodeResult.Success, code);
    }
}