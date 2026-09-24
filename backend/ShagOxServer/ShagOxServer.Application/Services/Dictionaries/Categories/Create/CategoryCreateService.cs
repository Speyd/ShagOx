using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public class CategoryCreateService 
    : ICategoryCreateService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly ProductTypeValidator _productTypeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryCreateService> _logger;


    public CategoryCreateService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        ProductTypeValidator productTypeValidator,
        IUnitOfWork unitOfWork,
        ILogger<CategoryCreateService> logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _productTypeValidator = productTypeValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CategoryCreateRequest request)
    {
        var typeValidator = await _productTypeValidator
           .ExistsByIdAsync(request.ProductTypeId);

        if (!typeValidator.IsSuccess)
            Result<CreateResponse>.Fail(typeValidator.Error);


        var nameValidator = await _categoryValidator
            .NotExistsAsync(request.Code, request.ProductTypeId);

        if (!nameValidator.IsSuccess)
            Result<CreateResponse>.Fail(nameValidator.Error);  


        var category = CategoryCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Add(category);

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

        var response = new CreateResponse(
            category.Id,
            DateTime.UtcNow
        );

        return Result<CreateResponse>.Success(response);
    }
}