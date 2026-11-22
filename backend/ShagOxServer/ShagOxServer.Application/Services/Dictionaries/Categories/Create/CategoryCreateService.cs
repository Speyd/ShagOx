using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Http;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public class CategoryCreateService 
    : ICategoryCreateService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly ProductTypeValidator _productTypeValidator;

    private readonly ICategoryImageCreateService _imageCreateService;


    private readonly CategoryInvalidationService _categoryInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryCreateService> _logger;


    public CategoryCreateService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        ProductTypeValidator productTypeValidator,
        CategoryInvalidationService categoryInvalidation,
        ICategoryImageCreateService imageCreateService,
        IUnitOfWork unitOfWork,
        ILogger<CategoryCreateService> logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _productTypeValidator = productTypeValidator;
        _categoryInvalidation = categoryInvalidation;
        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CategoryCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var typeValidator = await _productTypeValidator
           .ExistsByIdAsync(request.ProductTypeId, cancellationToken);

        if (!typeValidator.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(typeValidator.Error);
        }


        var nameValidator = await _categoryValidator
            .NotExistsAsync(request.Code, 
                request.ProductTypeId, cancellationToken);

        if (!nameValidator.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(nameValidator.Error);
        }


        var category = CategoryCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {   
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync();

            if (request.Picture is not null)
            {

                var imageResult = await _imageCreateService.CreateInternalAsync(
                       new PictureCreateRequest(category.Id, request.Picture),
                       cancellationToken);

                if (!imageResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<CreateResponse>
                        .Fail(imageResult.Error);
                }
            }

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create category. ProductTypeId: {ProductTypeId}",
               request.ProductTypeId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.CategoryCreateFailed);
        }

        await _categoryInvalidation.InvalidateCreateAsync(
            CategoryCacheMapper.ToInfo(category));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                category.Id,
                DateTime.UtcNow
        ));
    }

    private async Task<Result<bool>> CreatePicture(
        Category category,
        CategoryCreateRequest request,
        CancellationToken cancellationToken)
    {
        if(request.Picture is null)
            return Result<bool>.Success(true);

        var imageResult = await _imageCreateService.CreateInternalAsync(
               new PictureCreateRequest(category.Id, request.Picture),
               cancellationToken);

        if (!imageResult.IsSuccess)
        {
            return Result<bool>
                .Fail(imageResult.Error);
        }

        return Result<bool>.Success(true);
    }
}
