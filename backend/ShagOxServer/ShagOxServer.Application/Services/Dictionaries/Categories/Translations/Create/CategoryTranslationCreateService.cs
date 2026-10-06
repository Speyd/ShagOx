using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Create;
public class CategoryTranslationCreateService
    : ICategoryTranslationCreateService
{
    private readonly IRepository<CategoryTranslation> _categoryRepository;
    private readonly CategoryTranslationValidator _categoryValidator;

    private readonly CategoryTranslationInvalidationService _trnsInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryTranslationCreateService> _logger;


    public CategoryTranslationCreateService(
        IRepository<CategoryTranslation> categoryRepository,
        CategoryTranslationValidator categoryValidator,
        CategoryTranslationInvalidationService trnsInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CategoryTranslationCreateService> logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _trnsInvalid = trnsInvalid;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CategoryTranslationCreateRequest request)
    {
        var codeValidation = await _categoryValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var category = CategoryTranslationCreater
            .Create(request);

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
               "Failed to update category translation. " +
               "TranslatableId: {TranslatableId}",
               request.TranslatableId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.CategoryTranslationCreateFailed);
        }

        await _trnsInvalid.InvalidateCreateAsync(
            BaseTranslationCacheMapper.ToInfo(category));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                category.Id,
                DateTime.UtcNow
        ));
    }
}