using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Delete;
public class AdvertisementVariantDeleteService
    : IAdvertisementVariantDeleteService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantDeleteService> _logger;


    public AdvertisementVariantDeleteService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantDeleteService> logger)
    {
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var variant = await _variantValidator
            .GetByIdAsync(id);

        if (!variant.IsSuccess)
            return Result<DeleteResponse>.Fail(variant.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _variantRepository.Delete(variant.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete advertisement variant. Id: {Id}",
                id);

            return Result<DeleteResponse>
                     .Fail(EntityErrorResources.AdvertisementVariantDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               variant.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}