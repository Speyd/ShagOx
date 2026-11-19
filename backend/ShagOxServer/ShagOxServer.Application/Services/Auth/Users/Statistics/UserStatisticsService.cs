using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Statistics;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Statistics;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Statistics;
public class UserStatisticsService
    : IUserStatisticsService
{
    private readonly IUserStatisticsRepository _repository;

    public UserStatisticsService(
        IUserStatisticsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserStatisticsDto>> GetAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var statistics = await _repository
            .GetAsync(userId, cancellationToken);

        return Result<UserStatisticsDto>.Success(statistics);
    }
}
