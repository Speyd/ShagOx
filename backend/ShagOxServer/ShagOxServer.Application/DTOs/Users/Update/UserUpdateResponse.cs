

namespace ShagOxServer.Application.DTOs.Users.Update;
public sealed record UserUpdateResponse
(
    DateTime TimeUpdate,
    int CountUpdatedProperty
);