using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementExistsRepository 
    : ExistsOwnerRepository<Advertisement>, 
      IAdvertisementExistsRepository
{
    public AdvertisementExistsRepository(
        AppDbContext db)
        : base(db)
    { }
}
