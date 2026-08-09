using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IRepository<Advertisement> _advertisementRepository;
    private readonly AdvertisementCreateValidator _advertisementValidator;
 
    private readonly IImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementCreateService(
        IRepository<Advertisement> advertisementRepository,
        AdvertisementCreateValidator advertisementValidator,
        IImageCreateService imageCreateService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementValidator = advertisementValidator;

        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        int userId)
    {
        var validation = await _advertisementValidator
            .ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error!);
        

        var advert = AdvertisementCreater
            .Create(request, userId);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _advertisementRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            var imagesResult = await _imageCreateService
                .CreateFromFilesAsync(
                    new ImageFilesCreateRequest(
                        advert.Id, 
                        request.Images
                    )
            );

            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<CreateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                advert.Id,
                advert.CreatedAt
        ));
    }
}