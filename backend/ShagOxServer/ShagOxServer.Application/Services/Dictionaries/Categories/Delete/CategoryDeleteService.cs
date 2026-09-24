using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
public class CategoryDeleteService 
    : ICategoryDeleteService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryDeleteService> _logger;


    public CategoryDeleteService(
        IRepository<Category> categoryRepository,
        CategoryValidator categoryValidator,
        IUnitOfWork unitOfWork,
        ILogger<CategoryDeleteService> logger)
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
               "Failed to delete category. Id: {Id}",
               id);

            return Result<DeleteResponse>
                .Fail(EntityError.CategoryDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                category.Value!.Id,
                DateTime.UtcNow
            )
      );
    }
}