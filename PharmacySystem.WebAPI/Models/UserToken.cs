using System;
using System.Collections.Generic;

namespace PharmacySystem.WebAPI.Models
{
    public partial class UserToken
    {
        public Guid UserId { get; set; }
        public string? LoginProvider { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
