using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class SupplierGroupVM
    {
        public long IdSupplierGroup { get; set; }
        public string SupplierGroupName { get; set; }
        public string? Note { get; set; }
    }
}
