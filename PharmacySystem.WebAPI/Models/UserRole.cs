using System;
using System.Collections.Generic;

namespace PharmacySystem.WebAPI.Models
{
    public partial class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
