using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;
public class AvatarDeleteService
    : IAvatarDeleteService
{
    private readonly IRepository<Avatar> _avatarRepository;
    private readonly AvatarValidator _avatarValidator;
    private readonly IPictureLoaderService _loaderService;

    private readonly IUnitOfWork _unitOfWork;


    public AvatarDeleteService(
        IRepository<Avatar> avatarRepository,
        AvatarValidator avatarValidator,
        IPictureLoaderService loaderService,
        IUnitOfWork unitOfWork)
    {
        _avatarRepository = avatarRepository;
        _avatarValidator = avatarValidator;
        _loaderService = loaderService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var avatar = await _avatarValidator.GetByIdAsync(id);
        if (!avatar.IsSuccess)
            return Result<DeleteResponse>.Fail(avatar.Error);
 

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _avatarRepository.Delete(avatar.Value!);

            var result = await _loaderService
                .DeleteAsync(avatar.Value!.PublicId);
            
            if(!result.IsSuccess)
                throw new Exception(result.Error);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            return Result<DeleteResponse>
                .Fail("Failed to delete avatar.");
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               avatar.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}