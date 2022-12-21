namespace PharmacySystem.WebAdmin.ReportModels
{
    public partial class InvoiceDetail
    {
        public long IdInvoiceDetails { get; set; }

        public long? IdImportInvoice { get; set; }

        public long? IdExportInvoice { get; set; }

        public long IdMedicine { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

    }
}
