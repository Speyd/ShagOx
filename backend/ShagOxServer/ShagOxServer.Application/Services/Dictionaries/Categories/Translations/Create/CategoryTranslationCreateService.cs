using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Create;
public class CategoryTranslationCreateService
    : ICategoryTranslationCreateService
{
    private readonly IRepository<CategoryTranslation> _categoryRepository;
    private readonly CategoryTranslationValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryTranslationCreateService(
        IRepository<CategoryTranslation> categoryRepository,
        CategoryTranslationValidator categoryValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CategoryTranslationCreateRequest request)
    {
        var codeValidation = await _categoryValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var productType = CategoryTranslationCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _categoryRepository.Add(productType);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                productType.Id,
                DateTime.UtcNow
        ));
    }
}