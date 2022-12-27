using Microsoft.AspNetCore.Identity;

namespace PharmacySystem.WebAPI.Models
{
    public class Users : IdentityUser<Guid>
    {
        public long IdStaff { get; set; }
        public long IdAccount { get; set; }
    }
}
