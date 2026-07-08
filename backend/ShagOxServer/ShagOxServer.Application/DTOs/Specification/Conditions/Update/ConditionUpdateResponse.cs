namespace ShagOxServer.Application.DTOs.Specification.Conditions.Update;
public sealed record ConditionUpdateResponse
(
    DateTime TimeUpdate,
    int CountUpdatedProperty
);