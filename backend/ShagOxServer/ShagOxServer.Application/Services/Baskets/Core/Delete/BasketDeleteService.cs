using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Delete;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.Core.Delete;
public class BasketDeleteService
    : IBasketDeleteService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly BasketValidator _basketValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketDeleteService> _logger;


    public BasketDeleteService(
        IRepository<Basket> basketRepository,
        BasketValidator basketValidator,
        IUnitOfWork unitOfWork,
        ILogger<BasketDeleteService> logger)
    {
        _basketRepository = basketRepository;
        _basketValidator = basketValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var basket = await _basketValidator
            .GetByIdAsync(id);

        if (!basket.IsSuccess)
            return Result<DeleteResponse>.Fail(basket.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _basketRepository.Delete(basket.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete basket. Id: {Id}",
                id);

            return Result<DeleteResponse>
                     .Fail("Failed to delete basket.");
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               basket.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}