using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Delete;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Delete;
public class BasketAttributeDeleteService
    : IBasketAttributeDeleteService
{
    private readonly IRepository<BasketAttribute> _attributeRepository;
    private readonly BasketAttributeValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;


    public BasketAttributeDeleteService(
        IRepository<BasketAttribute> attributeRepository,
        BasketAttributeValidator attributeValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var attribute = await _attributeValidator
            .GetByIdAsync(id);

        if (!attribute.IsSuccess)
            return Result<DeleteResponse>.Fail(attribute.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}