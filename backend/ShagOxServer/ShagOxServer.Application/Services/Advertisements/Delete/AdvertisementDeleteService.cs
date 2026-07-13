using ShagOxServer.Application.DTOs.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Delete;
public class AdvertisementDeleteService : IAdvertisementDeleteService
{
    private readonly IAdvertisementRepository _repository;
    private readonly IAdvertisementQueryRepository _repositoryQuery;

    private readonly IImageDeleteService _imageService;

    private readonly IUnitOfWork _unitOfWork;

    public AdvertisementDeleteService(
        IAdvertisementRepository advertisementRepository,
        IAdvertisementQueryRepository advertisementQueryRepositor,
        IImageDeleteService imageService,
        IUnitOfWork unitOfWork)
    {
        _repository = advertisementRepository;
        _repositoryQuery = advertisementQueryRepositor;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AdvertisementDeleteResponse>> DeleteAdvertisementAsync(int id)
    {
        var advert = await _repositoryQuery.GetByIdAsync(id);
        if (advert is null)
            return Result<AdvertisementDeleteResponse>.NotFound("Advertisement");

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _repository.Delete(advert);

            foreach (var image in advert.Images)
            {
                await _imageService.DeleteImageAsync(image.Id);
            }

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<AdvertisementDeleteResponse>.Success(
           new AdvertisementDeleteResponse(
               advert.Id,
               DateTime.UtcNow
           )
       );
    }
}