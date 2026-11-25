namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class PaymentExpirationWorkerOptions
{
    public const string SectionName = "PaymentExpirationWorker";

    public TimeSpan PendingInterval { get; init; }
    public int CountPending { get; init; }
}
