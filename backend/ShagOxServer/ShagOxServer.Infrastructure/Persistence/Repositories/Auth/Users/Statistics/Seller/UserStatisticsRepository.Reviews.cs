using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<(
        int Count,
        double? AverageGrade,
        IReadOnlyList<GradeCountDto> Grades)> GetReviewStatisticsAsync(
        IQueryable<Advertisement> adverts,
        CancellationToken cancellationToken)
    {
        var reviews = Reviews()
            .Where(r => adverts.Any(a =>
                a.Id == r.CommentableEntityId));

        var grades = await reviews
            .GroupBy(r => r.Grade)
            .Select(g => new
            {
                Grade = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(
                x => x.Grade,
                x => x.Count,
                cancellationToken);

        var count = grades.Values.Sum();

        double? averageGrade = count == 0
            ? null
            : Math.Round(
                grades.Sum(x => x.Key * x.Value) / (double)count,
                2);

        var gradeCounts = Enumerable.Range(1, 5)
            .Reverse()
            .Select(grade => new GradeCountDto(
                grade,
                grades.GetValueOrDefault(grade)))
            .ToList();

        return (count, averageGrade, gradeCounts);
    }
}
