using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Create;
public class RegionTranslationCreateService
    : IRegionTranslationCreateService
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly RegionTranslationValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegionTranslationCreateService> _logger;


    public RegionTranslationCreateService(
        IRepository<RegionTranslation> regionRepository,
        RegionTranslationValidator regionValidator,
        IUnitOfWork unitOfWork,
        ILogger<RegionTranslationCreateService> logger)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        RegionTranslationCreateRequest request)
    {
        var codeValidation = await _regionValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var region = RegionTranslationCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _regionRepository.Add(region);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create region translation. " +
                "TranslatableId: {TranslatableId}",
                request.TranslatableId);

            return Result<CreateResponse>
                 .Fail("Failed to create region translation.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                region.Id,
                DateTime.UtcNow
        ));
    }
}