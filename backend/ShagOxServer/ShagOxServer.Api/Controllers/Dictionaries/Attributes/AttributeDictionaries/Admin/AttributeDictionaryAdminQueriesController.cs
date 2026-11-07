using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaries.Admin;

[ApiController]
[Route("api/admin/attribute-dictionaries")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryAdminQueriesController
    : BaseAdminQueryController<AttributeDictionaryDto,
        AttributeDictionarySearchFilter>
{
    public AttributeDictionaryAdminQueriesController(
        IAttributeDictionaryQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
