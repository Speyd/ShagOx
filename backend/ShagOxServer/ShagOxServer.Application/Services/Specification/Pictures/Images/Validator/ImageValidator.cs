using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
public class ImageValidator
    : BaseValidator<Image>
{
    public ImageValidator(
        IRepository<Image> imageRepository,
        IExistsRepository<Image> imageExistsRepository
    ) : base(imageRepository, imageExistsRepository)
    {
    }
}