using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Auth;

public class UserRoleConfiguration 
    : IEntityTypeConfiguration<UserRole>
{
    public void Configure(
        EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasIndex(x => new
        {
            x.UserId,
            x.EntityStatus,
            x.RoleId
        });
    }
}
