namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class PaymentExpirationWorkerOptions
{
    public TimeSpan PendingInterval { get; init; }
    public int CountPending { get; init; }
}
