using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public class AdvertisementCreateService 
    : IAdvertisementCreateService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementCreateValidator _advertValidator;
 
    private readonly IImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementCreateService> _logger;


    public AdvertisementCreateService(
        IRepository<Advertisement> advertRepository,
        AdvertisementCreateValidator advertValidator,
        IImageCreateService imageCreateService,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementCreateService> logger)
    {
        _advertRepository = advertRepository;
        _advertValidator = advertValidator;

        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        int userId)
    {
        var validation = await _advertValidator
            .ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);
        

        var advert = AdvertisementCreater
            .Create(request, userId);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Add(advert);

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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create advertisement. " +
                "UserId: {UserId}, Title: {Title}",
                userId,
                request.Title);

            return Result<CreateResponse>
                    .Fail(EntityError.AdvertisementCreateFailed);
        }

        _logger.LogInformation(
            "Advertisement created successfully. Id: {Id}",
            advert.Id);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                advert.Id,
                advert.CreatedAt
        ));
    }
}