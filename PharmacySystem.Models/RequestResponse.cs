using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models
{
    public class RequestResponse
    {
        public Code Status { get; set; }
        public string? Content { get; set; }
        public string? Message { get; set; }
    }
    public enum Code
    {
        Success = 0,
        Failed = 1
    }
}
