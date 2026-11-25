namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics;
public sealed record UserStatisticQueryOptions
{
    public int MaxItemsPerWidget { get; init; } = 5;
}
