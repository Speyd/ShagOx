using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
public class RegionTranslationValidator
    : BaseTranslationValidator<RegionTranslation>
{
    private readonly IRegionTranslationExistsRepository _regionExistsRepository;


    public RegionTranslationValidator(
        IRepository<RegionTranslation> regionRepository,
        IRegionTranslationExistsRepository regionExistsRepository
    ) : base(regionRepository, regionExistsRepository)
    {
        _regionExistsRepository = regionExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _regionExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(RegionTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _regionExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(RegionTranslation));
        }

        return Result<bool>.Success(true);
    }
}