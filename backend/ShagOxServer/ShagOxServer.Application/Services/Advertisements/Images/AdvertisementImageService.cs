using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
    : IAdvertisementImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly AdvertisementValidator _advertValidator;


    public AdvertisementImageService(
        IUnitOfWork unitOfWork,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService,
        IImageLoaderService imageLoaderService,
        AdvertisementValidator advertValidator)
    {
        _unitOfWork = unitOfWork;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
        _imageLoaderService = imageLoaderService;
        _advertValidator = advertValidator;
    }
}