using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Services.Auth.Users.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth;
public class RegisterService 
    : IRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserExistsRepository _userExistsRepository;
    private readonly UserCreater _userCreater;
    
    private readonly IUnitOfWork _unitOfWork;


    public RegisterService(
        IRepository<User> userRepository,
        IUserExistsRepository userExistsRepository,
        UserCreater userCreater,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userExistsRepository = userExistsRepository;
        _userCreater = userCreater;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var user = _userCreater.CreateUser(request);

            var exists = await _userExistsRepository
                .ExistsAsync(user.Email, user.Phone);

            if (exists)
                return Result<RegisterResponse>.Fail("User already exists");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _userCreater.AddDefaultRole(user);

                _userRepository.Add(user);         

                await _userCreater.SetDefaultName(user);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            await _unitOfWork.CommitAsync();

            return Result<RegisterResponse>.Success(
               new RegisterResponse(user)
            );
        }
        catch(Exception ex)
        {
            return Result<RegisterResponse>.Fail(ex.Message);
        }
    }
}