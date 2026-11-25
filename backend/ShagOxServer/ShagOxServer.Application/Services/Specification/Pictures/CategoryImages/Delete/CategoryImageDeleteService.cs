using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Delete;
public class CategoryImageDeleteService
    : PictureDeleteService<CategoryImage>,
    ICategoryImageDeleteService
{
    public CategoryImageDeleteService(
        IRepository<CategoryImage> imageRepository,
        ICommandRepository<CategoryImage> imageCommand,
        CategoryImageValidator imageValidator,
        PictureValidator pictureValidator,
        IUnitOfWork unitOfWork,
        ILogger<CategoryImageDeleteService> logger
    )
        : base(imageRepository,
            imageCommand,
            imageValidator,
            pictureValidator,
            unitOfWork,
            logger)
    {
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.DeleteAsync(id, cancellationToken);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.CategoryImageDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id,
        CancellationToken cancellationToken = default)
    {

        try
        {
            return await base.DeleteRecordAsync(id, cancellationToken);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.CategoryImageDeleteFailed);
        }
    }
}
