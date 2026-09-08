using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Delete;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Delete;
public class RegionTranslationDeleteService
    : IRegionTranslationDeleteService
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly RegionTranslationValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionTranslationDeleteService(
        IRepository<RegionTranslation> regionRepository,
        RegionTranslationValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var region = await _regionValidator
            .GetByIdAsync(id);

        if (!region.IsSuccess)
            return Result<DeleteResponse>.Fail(region.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Delete(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}