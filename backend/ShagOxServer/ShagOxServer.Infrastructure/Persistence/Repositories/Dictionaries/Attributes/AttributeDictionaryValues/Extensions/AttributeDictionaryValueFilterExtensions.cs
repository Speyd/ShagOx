using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
public static class AttributeDictionaryValueFilterExtensions
{
    public static IQueryable<AttributeDictionaryValue> Filter(
        this IQueryable<AttributeDictionaryValue> query,
         AttributeDictionaryValueSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.DictionaryId.HasValue)
        {
            query = query.Where(u => 
                u.DictionaryId != filter.DictionaryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u => u.Code != null &&
                EF.Functions.ILike(u.Code, $"%{filter.Code}%"));
        }

        return query;
    }
}