namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.Facets;
public sealed record AttributeFacetDto
(
    string Key,
    string Value,
    int Count
);
