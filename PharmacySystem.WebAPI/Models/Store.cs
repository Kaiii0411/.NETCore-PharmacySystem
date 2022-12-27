using System;
using System.Collections.Generic;

namespace PharmacySystem.WebAPI.Models
{
    public partial class Store
    {
        public Store()
        {
            staff = new HashSet<staff>();
        }

        public long IdStore { get; set; }
        public string StoreName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string StoreOwner { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<staff> staff { get; set; }
    }
}
