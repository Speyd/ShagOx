using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Base.Requests;
public sealed record ChangeStatusRequest
(
    EntityStatus ChangeStatus,
    EntityStatus FindStatus = EntityStatus.All
);
