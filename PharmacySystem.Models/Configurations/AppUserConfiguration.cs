using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.Models.Identity;

namespace PharmacySystem.WebAPI.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.IdStaff).IsRequired();
        }
    }
}
