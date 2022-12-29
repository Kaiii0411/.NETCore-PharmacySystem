using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetUsersPagingRequest
    {
        public string? UserName { get; set; } = null!;
        public long? IdAccount { get; set; } = null!;
    }
}
