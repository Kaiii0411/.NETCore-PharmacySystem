using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Store
    {
        public Store()
        {
            staff = new HashSet<staff>();
        }

        public long IdStore { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? StoreOwner { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
