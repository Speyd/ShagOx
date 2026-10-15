using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
    where TOwner: BaseEntity, IHasPictures<TOwner, TPicture>
    where TPicture : BasePictureOrder<TOwner>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPictureOrderCreateService<TPicture, PictureOrderCreateRequest> _pictureCreateService;
    private readonly IPictureDeleteService<TPicture> _pictureDeleteService;
    private readonly IPictureLoaderService _pictureLoaderService;
    private readonly BaseValidator<TOwner>  _ownerValidator;


    public PictureOrderingService(
        IUnitOfWork unitOfWork,
        IPictureOrderCreateService<TPicture, PictureOrderCreateRequest> pictureCreateService,
        IPictureDeleteService<TPicture> pictureDeleteService,
        IPictureLoaderService pictureLoaderService,
        BaseValidator<TOwner> ownerValidator)
    {
        _unitOfWork = unitOfWork;
        _pictureCreateService = pictureCreateService;
        _pictureDeleteService = pictureDeleteService;
        _pictureLoaderService = pictureLoaderService;
        _ownerValidator = ownerValidator;
    }
}
