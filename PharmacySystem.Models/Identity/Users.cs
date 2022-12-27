using Microsoft.AspNetCore.Identity;

namespace PharmacySystem.Models.Identity
{
    public class Users : IdentityUser<Guid>
    {
        public long IdStaff { get; set; }
        public long IdAccount { get; set; }
    }
}
