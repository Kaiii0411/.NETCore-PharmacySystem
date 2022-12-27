using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
