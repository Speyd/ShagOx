using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Create;
public partial class CategoryImageCreateService
    : PictureCreateService<Category,
        CategoryImage,
        PictureCreateRequest>,
    ICategoryImageCreateService
{
    public CategoryImageCreateService(
        IRepository<CategoryImage> imageRepository,
        PictureValidator pictureValidator,
        CategoryValidator categoryValidator,
        IUnitOfWork unitOfWork,
        ILogger<CategoryImageCreateService> logger
    )
        : base(imageRepository,
            pictureValidator,
            categoryValidator,
            unitOfWork,
            logger)
    {
    }


    public override async Task<Result<PictureCreateResponse>> CreateAsync(
        PictureCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.CreateAsync(request, cancellationToken);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.AvatarCreateFailed);
        }
    }

    public override async Task<Result<PictureCreateResponse>> CreateInternalAsync(
        PictureCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base
                .CreateInternalAsync(request, cancellationToken);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.CategoryImageCreateFailed);
        }
    }

    protected override CategoryImage CreatePicture(
        PictureCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return CategoryImageCreater
            .Create(request, response);
    }
}
