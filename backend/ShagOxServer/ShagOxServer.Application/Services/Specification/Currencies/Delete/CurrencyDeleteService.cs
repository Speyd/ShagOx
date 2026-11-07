using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService
    : BaseDeleteService<Currency>
    , ICurrencyDeleteService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;

    private readonly CurrencyInvalidationService _currencyInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CurrencyDeleteService> _logger;


    public CurrencyDeleteService(
        IRepository<Currency> currencyRepository,
        CurrencyValidator currencyValidator,
        CurrencyInvalidationService currencyInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CurrencyDeleteService> logger
    )
        : base(currencyValidator, unitOfWork, logger)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _currencyInvalid = currencyInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var currency = await _currencyValidator
            .GetByIdAsync(id, cancellationToken);
        if (!currency.IsSuccess)
            return Result<DeleteResponse>.Fail(currency.Error);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _currencyRepository.Delete(currency.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete currency. Id: {Id}",
               id);

            return Result<DeleteResponse>
                 .Fail(EntityErrorResources.CurrencyDeleteFailed);
        }

        await InvalidateCache(currency.Value!);

        return Result<DeleteResponse>.Success(
          new DeleteResponse(
              currency.Value!.Id,
              DateTime.UtcNow
          )
      );
    }

    public override async Task InvalidateCache(
        Currency entity)
    {
        await _currencyInvalid.InvalidateDeleteAsync(
            CurrencyCacheMapper.ToInfo(entity));
    }
}
