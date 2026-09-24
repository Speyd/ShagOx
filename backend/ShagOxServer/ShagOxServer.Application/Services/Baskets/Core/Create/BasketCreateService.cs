using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.Core.Create;
public class BasketCreateService
    : IBasketCreateService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly BasketValidator _basketValidator;

    private readonly UserValidator _userValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketCreateService> _logger;


    public BasketCreateService(
        IRepository<Basket> basketRepository,
        BasketValidator basketValidator,
        UserValidator userValidator,
        IUnitOfWork unitOfWork,
        ILogger<BasketCreateService> logger)
    {
        _basketRepository = basketRepository;
        _basketValidator = basketValidator;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketCreateRequest request)
    {
        var userExists = await _userValidator
            .ExistsByIdAsync(request.UserId);
        if (!userExists.IsSuccess)
            return Result<CreateResponse>.Fail(userExists.Error);

        var basketUserExists = await _basketValidator
            .NotExistsByUserAsync(request.UserId);
        if (!basketUserExists.IsSuccess)
            return Result<CreateResponse>.Fail(basketUserExists.Error);


        var basket = BasketCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _basketRepository.Add(basket);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create basket. UserId: {UserId}",
                request.UserId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.BasketCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                basket.Id,
                DateTime.UtcNow
        ));
    }
}