using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Account
    {
        public Account()
        {
            ExportInvoices = new HashSet<ExportInvoice>();
            ImportInvoices = new HashSet<ImportInvoice>();
        }

        public long IdAccount { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public long? IdStaff { get; set; }
        public int? TypeAccount { get; set; }

        public virtual staff? IdStaffNavigation { get; set; }
        public virtual ICollection<ExportInvoice> ExportInvoices { get; set; }
        public virtual ICollection<ImportInvoice> ImportInvoices { get; set; }
    }
}
