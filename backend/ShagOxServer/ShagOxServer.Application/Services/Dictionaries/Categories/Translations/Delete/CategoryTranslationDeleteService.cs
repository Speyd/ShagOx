using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Http;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Delete;
public class CategoryTranslationDeleteService
    : ICategoryTranslationDeleteService
{
    private readonly IRepository<CategoryTranslation> _categoryRepository;
    private readonly CategoryTranslationValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryTranslationDeleteService> _logger;


    public CategoryTranslationDeleteService(
        IRepository<CategoryTranslation> categoryRepository,
        CategoryTranslationValidator categoryValidator,
        IUnitOfWork unitOfWork,
        ILogger<CategoryTranslationDeleteService> logger)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var category = await _categoryValidator
            .GetByIdAsync(id);

        if (!category.IsSuccess)
            return Result<DeleteResponse>.Fail(category.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Delete(category.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete category translation. Id: {id}",
               id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.CategoryTranslationDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               category.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}