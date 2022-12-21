using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PharmacySystem.Models.ReportModels;

namespace PharmacySystem.Models
{
    public partial class PharmacySystemContext : DbContext
    {
        public PharmacySystemContext()
        {
        }

        public PharmacySystemContext(DbContextOptions<PharmacySystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<ExportInvoice> ExportInvoices { get; set; } = null!;
        public virtual DbSet<ImportInvoice> ImportInvoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Medicine> Medicines { get; set; } = null!;
        public virtual DbSet<MedicineGroup> MedicineGroups { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<SupplierGroup> SupplierGroups { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        //
        public virtual DbSet<IInvoiceReportModels> IInvoiceReportModels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-O9JPNLU\\SQLEXPRESS;Database=PharmacySystem;User Id=sa;Password=0411;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount);

                entity.ToTable("Account");

                entity.Property(e => e.Password).HasMaxLength(1000);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdStaff)
                    .HasConstraintName("FK_Account_Staff");
            });

            modelBuilder.Entity<ExportInvoice>(entity =>
            {
                entity.HasKey(e => e.IdExportInvoice);

                entity.ToTable("ExportInvoice");

                entity.Property(e => e.DateCheckIn).HasColumnType("datetime");

                entity.Property(e => e.DateCheckOut).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.ExportInvoices)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExportInvoice_Account");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ExportInvoices)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExportInvoice_Status");
            });

            modelBuilder.Entity<ImportInvoice>(entity =>
            {
                entity.HasKey(e => e.IdImportInvoice);

                entity.ToTable("ImportInvoice");

                entity.Property(e => e.DateCheckIn).HasColumnType("datetime");

                entity.Property(e => e.DateCheckOut).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.ImportInvoices)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportInvoice_Account");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.ImportInvoices)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportInvoice_Supplier");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ImportInvoices)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportInvoice_Status");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.IdInvoiceDetails);

                entity.HasOne(d => d.IdExportInvoiceNavigation)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.IdExportInvoice)
                    .HasConstraintName("FK_InvoiceDetails_ExportInvoice");

                entity.HasOne(d => d.IdImportInvoiceNavigation)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.IdImportInvoice)
                    .HasConstraintName("FK_InvoiceDetails_ImportInvoice");

                entity.HasOne(d => d.IdMedicineNavigation)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.IdMedicine)
                    .HasConstraintName("FK_InvoiceDetails_Medicine");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.IdMedicine);

                entity.ToTable("Medicine");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.MedicineName).HasMaxLength(200);

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.IdMedicineGroupNavigation)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.IdMedicineGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicine_MedicineGroup");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicine_Supplier");
            });

            modelBuilder.Entity<MedicineGroup>(entity =>
            {
                entity.HasKey(e => e.IdMedicineGroup);

                entity.ToTable("MedicineGroup");

                entity.Property(e => e.MedicineGroupName).HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(300);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusColor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusDescription).HasMaxLength(500);

                entity.Property(e => e.StatusName).HasMaxLength(150);

                entity.Property(e => e.StatusText)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.IdStore);

                entity.ToTable("Store");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.StoreName).HasMaxLength(200);

                entity.Property(e => e.StoreOwner).HasMaxLength(100);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.IdSupplier);

                entity.ToTable("Supplier");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.SupplierName).HasMaxLength(200);

                entity.HasOne(d => d.IdSupplierGroupNavigation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.IdSupplierGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_SupplierGroup");
            });

            modelBuilder.Entity<SupplierGroup>(entity =>
            {
                entity.HasKey(e => e.IdSupplierGroup);

                entity.ToTable("SupplierGroup");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.SupplierGroupName).HasMaxLength(200);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.IdStaff);

                entity.ToTable("Staff");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.StaffName).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK_Staff_Store");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
