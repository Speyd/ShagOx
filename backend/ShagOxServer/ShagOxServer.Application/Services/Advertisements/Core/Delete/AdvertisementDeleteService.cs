using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Delete;
public class AdvertisementDeleteService
    : BaseDeleteService<Advertisement>
    ,IAdvertisementDeleteService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementValidator _advertValidator;

    private readonly IAdvertisementImageDeleteService _imageDeleteService;
    private readonly IPictureLoaderService _loaderService;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementDeleteService> _logger;


    public AdvertisementDeleteService(
        IRepository<Advertisement> advertRepository,
        AdvertisementValidator advertValidator,
        IAdvertisementImageDeleteService imageDeleteService,
        IPictureLoaderService loaderService,
        AdvertisementInvalidationService advertInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementDeleteService> logger
    )
        : base(advertValidator, unitOfWork, logger)
    {
        _advertRepository = advertRepository;
        _advertValidator = advertValidator;
        _imageDeleteService = imageDeleteService;
        _loaderService = loaderService;
        _advertInvalid = advertInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var advert = await _advertValidator
            .GetByIdAsync(id, cancellationToken);

        if (!advert.IsSuccess)
            return Result<DeleteResponse>.Fail(advert.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Delete(advert.Value!);

            foreach (var image in advert.Value!.Pictures)
            {
                await _imageDeleteService
                    .DeleteRecordAsync(image.Id, cancellationToken);
            }

            await _unitOfWork.CommitAsync();

            foreach (var image in advert.Value!.Pictures)
            {
                await _loaderService
                    .DeleteAsync(image.PublicId);
            }

        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete advertisement. Id: {Id}",
                id);

            return Result<DeleteResponse>
                    .Fail(EntityErrorResources.AdvertisementDeleteFailed);
        }

        _logger.LogInformation(
            "Advertisement deleted successfully. Id: {Id}",
            advert.Value!.Id);

        await InvalidateCache(advert.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               advert.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task<Result<bool>> SoftBodyDeleteAsync(
        Advertisement entity,
        CancellationToken cancellationToken = default)
    {
        var body = await base.SoftBodyDeleteAsync(entity,
            cancellationToken);

        if (!body.IsSuccess)
            Result<bool>.Fail(body.Error);

        foreach (var image in entity.Pictures)
        {
            var imageDelete = await _imageDeleteService
                .SoftDeleteAsync(image.Id, cancellationToken);

            if (!imageDelete.IsSuccess)
                Result<bool>.Fail(imageDelete.Error);
        }

        return Result<bool>.Success(true);
    }

    public override async Task InvalidateCache(
        Advertisement entity)
    {
        await _advertInvalid.InvalidateDeleteAsync(
            AdvertisementCacheMapper.ToInfo(entity));
    }
}
