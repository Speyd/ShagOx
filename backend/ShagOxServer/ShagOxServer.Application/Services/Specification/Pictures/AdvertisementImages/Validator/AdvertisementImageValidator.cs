using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
public class AdvertisementImageValidator
    : BaseValidator<AdvertisementImage>
{
    public AdvertisementImageValidator(
        IQueryRepository<AdvertisementImage> imageRepository,
        IExistsRepository<AdvertisementImage> imageExistsRepository
    ) : base(imageRepository, imageExistsRepository)
    {
    }
}
