using ShagOxServer.Application.DTOs.Auth.Users.Statistics;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Statistics;
public interface IUserStatisticsRepository
{
    Task<UserStatisticsDto> GetByUserAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken = default);
}
