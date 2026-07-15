using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly AdvertisementCreateValidator _advertisementValidator;
 
    private readonly IImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        AdvertisementCreateValidator advertisementValidator,
        IImageCreateService imageCreateService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementValidator = advertisementValidator;

        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AdvertisementCreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        int userId)
    {
        var validation = await _advertisementValidator.ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);
        
        var advert = AdvertisementCreater.CreateAdvertisement(request, userId);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _advertisementRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            var imagesResult = await _imageCreateService.CreateFromFilesAsync(
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