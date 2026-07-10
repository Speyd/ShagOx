using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConditionRepository _conditionRepository;
    private readonly IImageCreateService _imageService;
    private readonly IImageLoaderService _loaderService;
    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IConditionRepository conditionRepository,
        IImageCreateService imageService,
        IImageLoaderService loaderService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
        _imageService = imageService;
        _loaderService = loaderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
        AdvertisementCreateRequest request)
    {
        var validation = await ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);

        var advert = CreateAdvertisement(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _advertisementRepository.AddAsync(advert);

            await _unitOfWork.SaveChangesAsync();


            var imagesResult = await CreateImagesAsync(
                advert.Id,
                request.Images);


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

        var response = new AdvertisementCreateResponse(
            advert.Id,
            advert.CreatedAt
        );

        return Result<AdvertisementCreateResponse>.Success(response);
    }

    private async Task<Result<bool>> ValidateAsync(
        AdvertisementCreateRequest request)
    {
        var seller = await _userRepository.GetByIdAsync(request.SellerId);
        if (seller is null)
            return Result<bool>
                .NotFound("Seller");

        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            return Result<bool>
                .NotFound("Currency");

        var condition = await _conditionRepository.GetByIdAsync(request.ConditionId);
        if (condition is null)
            return Result<bool>
                .NotFound("Condition");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<bool>
                .NotFound("Category");

        return Result<bool>
            .Success(true);
    }

    private Advertisement CreateAdvertisement(
        AdvertisementCreateRequest request)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,
            ConditionId = request.ConditionId,
            SellerId = request.SellerId,

            Properties = request.Properties ?? new()
        };
    }

    private async Task<Result<bool>> CreateImagesAsync(
        int advertisementId,
        List<IFormFile> images)
    {
        var uploadedImages = new List<string>();

        foreach (var image in images)
        {
            var result = await _imageService.CreateFromFileInternalAsync(
                new ImageFileCreateRequest(
                    image,
                    advertisementId));


            if (!result.IsSuccess)
            {
                foreach (var publicId in uploadedImages)
                {
                    await _loaderService.DeleteAsync(publicId);
                }

                return Result<bool>
                    .Fail(result.Error!);
            }


            uploadedImages.Add(result.Value!.PublicId);
        }


        return Result<bool>.Success(true);
    }
}