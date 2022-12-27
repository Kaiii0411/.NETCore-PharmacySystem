using Microsoft.AspNetCore.Identity;

namespace PharmacySystem.WebAPI.Models
{
    public class Roles : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
