using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Create;
public class BasketAttributeCreateService
    : IBasketAttributeCreateService
{
    private readonly IRepository<BasketAttribute> _attributeRepository;
    private readonly BasketAttributeValidator _attributeValidator;

    private readonly AttributeDefinitionValidator _attributeDefinitionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public BasketAttributeCreateService(
        IRepository<BasketAttribute> attributeRepository,
        BasketAttributeValidator attributeValidator,
        AttributeDefinitionValidator attributeDefinitionValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeDefinitionValidator = attributeDefinitionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketAttributeCreateRequest request)
    {
        var attributeDefinitionExists = await _attributeDefinitionValidator
            .ExistsByIdAsync(request.AttributeDefinitionId);

        if (!attributeDefinitionExists.IsSuccess)
            return Result<CreateResponse>.Fail(attributeDefinitionExists.Error);


        var keyExists = await _attributeValidator
            .NotExistsAsync(request.AttributeDefinitionId, request.Order);

        if (!keyExists.IsSuccess)
            return Result<CreateResponse>.Fail(keyExists.Error);


        var attribute = BasketAttributeCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}