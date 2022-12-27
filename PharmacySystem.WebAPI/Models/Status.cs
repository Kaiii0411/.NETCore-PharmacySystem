using System;
using System.Collections.Generic;

namespace PharmacySystem.WebAPI.Models
{
    public partial class Status
    {
        public Status()
        {
            ExportInvoices = new HashSet<ExportInvoice>();
            ImportInvoices = new HashSet<ImportInvoice>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public bool? IsActive { get; set; }
        public string? StatusColor { get; set; }
        public string? StatusText { get; set; }

        public virtual ICollection<ExportInvoice> ExportInvoices { get; set; }
        public virtual ICollection<ImportInvoice> ImportInvoices { get; set; }
    }
}
