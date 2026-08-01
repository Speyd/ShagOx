using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public class CurrencyCreateService : ICurrencyCreateService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly CurrencyCreateValidator _currencyCreateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyCreateService(
        ICurrencyRepository currencyRepository,
        CurrencyCreateValidator currencyCreateValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyCreateValidator = currencyCreateValidator;
        _unitOfWork = unitOfWork;
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                currency.Id,
                DateTime.UtcNow
        ));
    }
}