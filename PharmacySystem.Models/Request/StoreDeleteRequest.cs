using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class StoreDeleteRequest
    {
        public long IdStore { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? StoreOwner { get; set; }
        public string? Phone { get; set; }
    }
}
