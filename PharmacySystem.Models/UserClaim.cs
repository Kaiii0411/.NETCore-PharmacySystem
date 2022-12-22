using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class UserClaim
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
