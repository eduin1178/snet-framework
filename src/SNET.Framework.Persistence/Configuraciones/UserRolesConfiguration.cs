using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SNET.Framework.Domain.Entities;

namespace SNET.Framework.Persistence.Configuraciones;

internal class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(x => new
        {
            x.RoleId,
            x.UserId,
        });
    }
}
