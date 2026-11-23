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

    public async Task<Result<UserStatisticsDto>> GetByUserAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        var statistics = await _repository
            .GetByUserAsync(userId, options, cancellationToken);

        return Result<UserStatisticsDto>.Success(statistics);
    }
}
