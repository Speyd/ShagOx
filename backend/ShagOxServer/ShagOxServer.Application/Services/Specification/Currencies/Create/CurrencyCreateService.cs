using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public class CurrencyCreateService : ICurrencyCreateService
{
    private readonly ICurrencyRepository _repository;
    private readonly ICurrencyExistsRepository _existsRepository;


    public CurrencyCreateService(
        ICurrencyRepository currencyRepository,
        ICurrencyExistsRepository existsRepository)
    {
        _repository = currencyRepository;
        _existsRepository = existsRepository;
    }

    public async Task<Result<CurrencyCreateResponse>> CreateCurrencyAsync(
        CurrencyCreateRequest request)
    {
        if (await _existsRepository.ExistsByCodeAsync(request.Code))
            return Result<CurrencyCreateResponse>
                .AlreadyExists("Currency code");

        if (await _existsRepository.ExistsByNameAsync(request.Name))
            return Result<CurrencyCreateResponse>
                .AlreadyExists("Currency name");


        var currency = CreateCurrency(request);

        await _repository.AddAsync(currency);

        var response = new CurrencyCreateResponse(
            currency.Id,
            DateTime.UtcNow
        );

        return Result<CurrencyCreateResponse>.Success(response);
    }

    private Currency CreateCurrency(
        CurrencyCreateRequest request)
    {
        return new Currency
        {
            Code = request.Code,
            Symbol = request.Symbol,
            Name = request.Name,
        };
    }
}
