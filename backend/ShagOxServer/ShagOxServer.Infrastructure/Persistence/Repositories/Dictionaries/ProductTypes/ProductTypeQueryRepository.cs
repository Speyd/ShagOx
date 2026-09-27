using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using Twilio.TwiML.Voice;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
public class ProductTypeQueryRepository
    : QueryRepository<ProductType, ProductTypeSearchFilter>, 
      IProductTypeQueryRepository
{
    public ProductTypeQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<ProductType> ApplyFilter(
        IQueryable<ProductType> query,
        ProductTypeSearchFilter filter)
    {
        return query.Filter(filter);
    }
}