using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public class CurrencyCreateService
    : ICurrencyCreateService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly CurrencyCreateValidator _currencyCreateValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CurrencyCreateService> _logger;


    public CurrencyCreateService(
        IRepository<Currency> currencyRepository,
        CurrencyCreateValidator currencyCreateValidator,
        IUnitOfWork unitOfWork,
        ILogger<CurrencyCreateService> logger)
    {
        _currencyRepository = currencyRepository;
        _currencyCreateValidator = currencyCreateValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CurrencyCreateRequest request)
    {
        var code = await _currencyCreateValidator
            .ExistsByCodeValidator(request.Code);

        if (!code.IsSuccess)
            return Result<CreateResponse>.Fail(code.Error);


        var name = await _currencyCreateValidator
            .ExistsByNameValidator(request.Name);

        if (!name.IsSuccess)
            return Result<CreateResponse>.Fail(code.Error);

        var currency = CurrencyCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _currencyRepository.Add(currency);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create currency. Code: {Code}",
                request.Code);

            return Result<CreateResponse>
                 .Fail(EntityErrorResources.CurrencyCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                currency.Id,
                DateTime.UtcNow
        ));
    }
}