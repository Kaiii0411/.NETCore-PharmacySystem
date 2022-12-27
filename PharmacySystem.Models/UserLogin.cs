using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class UserLogin
    {
        public Guid UserId { get; set; }
        public string? LoginProvider { get; set; }
        public string? ProviderKey { get; set; }
        public string? ProviderDisplayName { get; set; }
    }
}
