using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService 
    : ICurrencyDeleteService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CurrencyDeleteService> _logger;


    public CurrencyDeleteService(
        IRepository<Currency> currencyRepository,
        CurrencyValidator currencyValidator,
        IUnitOfWork unitOfWork,
        ILogger<CurrencyDeleteService> logger)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var currency = await _currencyValidator.GetByIdAsync(id);
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
                 .Fail(EntityError.CurrencyDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
          new DeleteResponse(
              currency.Value!.Id,
              DateTime.UtcNow
          )
      );
    }
}