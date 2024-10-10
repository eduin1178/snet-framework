using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SNET.Framework.Domain.Entities;

namespace SNET.Framework.Persistence;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Users");

        builder.HasMany(x=>x.Roles)
            .WithOne(x=>x.User)
            .HasForeignKey(x=>x.UserId);
    }
}
