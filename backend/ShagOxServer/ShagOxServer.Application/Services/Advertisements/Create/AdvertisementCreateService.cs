using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly AdvertisementCreateValidator _validator;
 
    private readonly IImageCreateService _imageService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        AdvertisementCreateValidator validator,
        IImageCreateService imageService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _validator = validator;
       
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
        AdvertisementCreateRequest request,
        int userId)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);

        var advert = AdvertisementCreator.CreateAdvertisement(request, userId);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _advertisementRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            var imagesResult = await _imageService.CreateFromFilesAsync(
                new ImageFilesCreateRequest(advert.Id, request.Images)
            );

            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<AdvertisementCreateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<AdvertisementCreateResponse>.Success(
            new AdvertisementCreateResponse(
            advert.Id,
            advert.CreatedAt
        ));
    }
}