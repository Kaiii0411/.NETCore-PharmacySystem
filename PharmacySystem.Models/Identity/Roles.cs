using Microsoft.AspNetCore.Identity;

namespace PharmacySystem.Models.Identity
{
    public class Roles : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
