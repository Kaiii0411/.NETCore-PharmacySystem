using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class RevenueVM
    {
        public double TotalPriceYesterday { get; set; }
        public double TotalPriceNow { get; set; }
        public int InvoiceTotal { get; set; }
        public double PercentDifference {get; set;}
    }
}
