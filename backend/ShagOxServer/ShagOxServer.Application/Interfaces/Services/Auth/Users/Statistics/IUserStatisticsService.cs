using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Statistics;
public interface IUserStatisticsService
{
    Task<Result<UserStatisticsDto>> GetByUserAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken = default);
}
