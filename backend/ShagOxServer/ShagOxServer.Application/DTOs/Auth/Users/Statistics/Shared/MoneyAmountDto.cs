namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics.Shared;
public sealed record MoneyAmountDto
(
    string CurrencyCode,
    string Symbol,
    decimal Amount
);
