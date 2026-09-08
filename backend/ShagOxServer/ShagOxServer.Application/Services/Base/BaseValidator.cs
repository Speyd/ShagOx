using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base;
public abstract class BaseValidator<TObject>
    where TObject : class
{
    private readonly IRepository<TObject> _objectRepository;
    private readonly IExistsRepository<TObject> _objectExistsRepository;


    public BaseValidator(
        IRepository<TObject> objectRepository,
        IExistsRepository<TObject> objectExistsRepository)
    {
        _objectRepository = objectRepository;
        _objectExistsRepository = objectExistsRepository;
    }


    public async Task<Result<TObject>> GetByIdAsync(
        int objcetId)
    {
        var objcet = await _objectRepository
            .GetByIdAsync(objcetId);

        if (objcet is null)
        {
            return Result<TObject>
                .NotFound(typeof(TObject).Name);
        }

        return Result<TObject>.Success(objcet);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int objcetId)
    {
        if (!await _objectExistsRepository
            .ExistsByIdAsync(objcetId))
        {
            return Result<bool>
                .NotFound(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int objcetId)
    {
        if (await _objectExistsRepository
            .ExistsByIdAsync(objcetId))
        {
            return Result<bool>
                .AlreadyExists(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }
}