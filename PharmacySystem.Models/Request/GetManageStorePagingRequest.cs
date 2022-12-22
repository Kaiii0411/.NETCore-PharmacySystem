using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetManageStorePagingRequest
    {
        public long? IdStore { get; set; }
        public string? StoreName { get; set; }
    }
}
