using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.WebAPI.Models;

namespace PharmacySystem.WebAPI.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles");

            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();

        }
    }
}
