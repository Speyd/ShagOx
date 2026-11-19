namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class WebhookWorkerOptions
{
    public TimeSpan PendingInterval { get; init; }
}
