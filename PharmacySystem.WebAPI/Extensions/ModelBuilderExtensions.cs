using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.WebAPI.Models;

namespace PharmacySystem.WebAPI.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Roles>().HasData(new Roles
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "Admin",
                Description = "Administrator Role"
            });

            var hasher = new PasswordHasher<Users>();
            modelBuilder.Entity<Users>().HasData(new Users
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "nguyenducnghia4112@gmail.com",
                NormalizedEmail = "nguyenducnghia4112@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                IdAccount = 1,
                IdStaff = 1
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
