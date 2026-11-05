using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues.Admin;
[ApiController]
[Route("api/admin/attribute-dictionary-values")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryValueAdminQueriesController
    : BaseAdminQueryController<AttributeDictionaryValueDto,
        AttributeDictionaryValueSearchFilter>
{
    public AttributeDictionaryValueAdminQueriesController(
        IAttributeDictionaryValueQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
