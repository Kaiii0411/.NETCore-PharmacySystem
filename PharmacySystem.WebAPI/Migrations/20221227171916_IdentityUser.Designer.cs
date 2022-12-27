﻿//// <auto-generated />
//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using PharmacySystem.WebAPI.Models;

//#nullable disable

//namespace PharmacySystem.WebAPI.Migrations
//{
//    [DbContext(typeof(PharmacySystemContexts))]
//    [Migration("20221227171916_IdentityUser")]
//    partial class IdentityUser
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "6.0.12")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<Guid>("RoleId")
//                        .HasColumnType("uniqueidentifier");

//                    b.HasKey("Id");

//                    b.ToTable("RoleClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<Guid>("UserId")
//                        .HasColumnType("uniqueidentifier");

//                    b.HasKey("Id");

//                    b.ToTable("UserClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
//                {
//                    b.Property<Guid>("UserId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ProviderDisplayName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ProviderKey")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId");

//                    b.ToTable("UserLogins", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
//                {
//                    b.Property<Guid>("UserId")
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<Guid>("RoleId")
//                        .HasColumnType("uniqueidentifier");

//                    b.HasKey("UserId", "RoleId");

//                    b.ToTable("UserRoles", (string)null);

//                    b.HasData(
//                        new
//                        {
//                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
//                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
//                        });
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
//                {
//                    b.Property<Guid>("UserId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Value")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId");

//                    b.ToTable("UserTokens", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ExportInvoice", b =>
//                {
//                    b.Property<long>("IdExportInvoice")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdExportInvoice"), 1L, 1);

//                    b.Property<DateTime?>("DateCheckIn")
//                        .HasColumnType("datetime");

//                    b.Property<DateTime?>("DateCheckOut")
//                        .HasColumnType("datetime");

//                    b.Property<long>("IdAccount")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdAccountNavigationId")
//                        .HasColumnType("bigint");

//                    b.Property<string>("Note")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.Property<int>("StatusId")
//                        .HasColumnType("int")
//                        .HasColumnName("StatusID");

//                    b.HasKey("IdExportInvoice");

//                    b.HasIndex("IdAccountNavigationId");

//                    b.HasIndex("StatusId");

//                    b.ToTable("ExportInvoice", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ImportInvoice", b =>
//                {
//                    b.Property<long>("IdImportInvoice")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdImportInvoice"), 1L, 1);

//                    b.Property<DateTime?>("DateCheckIn")
//                        .HasColumnType("datetime");

//                    b.Property<DateTime?>("DateCheckOut")
//                        .HasColumnType("datetime");

//                    b.Property<long>("IdAccount")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdAccountNavigationId")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdSupplier")
//                        .HasColumnType("bigint");

//                    b.Property<string>("Note")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.Property<int>("StatusId")
//                        .HasColumnType("int")
//                        .HasColumnName("StatusID");

//                    b.HasKey("IdImportInvoice");

//                    b.HasIndex("IdAccountNavigationId");

//                    b.HasIndex("IdSupplier");

//                    b.HasIndex("StatusId");

//                    b.ToTable("ImportInvoice", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.InvoiceDetail", b =>
//                {
//                    b.Property<long>("IdInvoiceDetails")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdInvoiceDetails"), 1L, 1);

//                    b.Property<long?>("IdExportInvoice")
//                        .HasColumnType("bigint");

//                    b.Property<long?>("IdImportInvoice")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdMedicine")
//                        .HasColumnType("bigint");

//                    b.Property<int>("Quantity")
//                        .HasColumnType("int");

//                    b.Property<double>("TotalPrice")
//                        .HasColumnType("float");

//                    b.HasKey("IdInvoiceDetails");

//                    b.HasIndex("IdExportInvoice");

//                    b.HasIndex("IdImportInvoice");

//                    b.HasIndex("IdMedicine");

//                    b.ToTable("InvoiceDetails");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Medicine", b =>
//                {
//                    b.Property<long>("IdMedicine")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdMedicine"), 1L, 1);

//                    b.Property<string>("Description")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.Property<DateTime>("ExpiryDate")
//                        .HasColumnType("datetime");

//                    b.Property<long>("IdMedicineGroup")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdSupplier")
//                        .HasColumnType("bigint");

//                    b.Property<double>("ImportPrice")
//                        .HasColumnType("float");

//                    b.Property<string>("MedicineName")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<long>("Quantity")
//                        .HasColumnType("bigint");

//                    b.Property<double>("SellPrice")
//                        .HasColumnType("float");

//                    b.Property<string>("Unit")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .HasColumnType("nvarchar(50)");

//                    b.HasKey("IdMedicine");

//                    b.HasIndex("IdMedicineGroup");

//                    b.HasIndex("IdSupplier");

//                    b.ToTable("Medicine", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.MedicineGroup", b =>
//                {
//                    b.Property<long>("IdMedicineGroup")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdMedicineGroup"), 1L, 1);

//                    b.Property<string>("MedicineGroupName")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<string>("Note")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.HasKey("IdMedicineGroup");

//                    b.ToTable("MedicineGroup", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Role", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

//                    b.Property<string>("ConcurrencyStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("NormalizedName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Role");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.RoleClaim", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<long>("RoleId")
//                        .HasColumnType("bigint");

//                    b.HasKey("Id");

//                    b.HasIndex(new[] { "RoleId" }, "IX_RoleClaims_RoleId");

//                    b.ToTable("RoleClaim");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.staff", b =>
//                {
//                    b.Property<long>("IdStaff")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdStaff"), 1L, 1);

//                    b.Property<DateTime?>("DateOfBirth")
//                        .HasColumnType("datetime");

//                    b.Property<string>("Email")
//                        .HasMaxLength(100)
//                        .HasColumnType("nvarchar(100)");

//                    b.Property<long?>("IdStore")
//                        .HasColumnType("bigint");

//                    b.Property<string>("Phone")
//                        .HasMaxLength(15)
//                        .HasColumnType("nvarchar(15)");

//                    b.Property<string>("StaffName")
//                        .HasMaxLength(100)
//                        .HasColumnType("nvarchar(100)");

//                    b.HasKey("IdStaff");

//                    b.HasIndex("IdStore");

//                    b.ToTable("Staff", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Status", b =>
//                {
//                    b.Property<int>("StatusId")
//                        .HasColumnType("int")
//                        .HasColumnName("StatusID");

//                    b.Property<bool?>("IsActive")
//                        .HasColumnType("bit");

//                    b.Property<string>("StatusColor")
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)");

//                    b.Property<string>("StatusDescription")
//                        .HasMaxLength(500)
//                        .HasColumnType("nvarchar(500)");

//                    b.Property<string>("StatusName")
//                        .HasMaxLength(150)
//                        .HasColumnType("nvarchar(150)");

//                    b.Property<string>("StatusText")
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)");

//                    b.HasKey("StatusId");

//                    b.ToTable("Status", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Store", b =>
//                {
//                    b.Property<long>("IdStore")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdStore"), 1L, 1);

//                    b.Property<string>("Address")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<string>("Phone")
//                        .IsRequired()
//                        .HasMaxLength(15)
//                        .HasColumnType("nvarchar(15)");

//                    b.Property<string>("StoreName")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<string>("StoreOwner")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .HasColumnType("nvarchar(100)");

//                    b.HasKey("IdStore");

//                    b.ToTable("Store", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Supplier", b =>
//                {
//                    b.Property<long>("IdSupplier")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdSupplier"), 1L, 1);

//                    b.Property<string>("Address")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .HasColumnType("nvarchar(100)");

//                    b.Property<long>("IdSupplierGroup")
//                        .HasColumnType("bigint");

//                    b.Property<string>("Phone")
//                        .IsRequired()
//                        .HasMaxLength(15)
//                        .HasColumnType("nvarchar(15)");

//                    b.Property<string>("SupplierName")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.HasKey("IdSupplier");

//                    b.HasIndex("IdSupplierGroup");

//                    b.ToTable("Supplier", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.SupplierGroup", b =>
//                {
//                    b.Property<long>("IdSupplierGroup")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdSupplierGroup"), 1L, 1);

//                    b.Property<string>("Note")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.Property<string>("SupplierGroupName")
//                        .IsRequired()
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.HasKey("IdSupplierGroup");

//                    b.ToTable("SupplierGroup", (string)null);
//                });

//            modelBuilder.Entity("PharmacySystem.Models.User", b =>
//                {
//                    b.Property<long>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("bigint");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

//                    b.Property<int>("AccessFailedCount")
//                        .HasColumnType("int");

//                    b.Property<string>("ConcurrencyStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("EmailConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<long>("IdStaff")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdStaffNavigationIdStaff")
//                        .HasColumnType("bigint");

//                    b.Property<bool>("LockoutEnabled")
//                        .HasColumnType("bit");

//                    b.Property<DateTimeOffset?>("LockoutEnd")
//                        .HasColumnType("datetimeoffset");

//                    b.Property<string>("NormalizedEmail")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("NormalizedUserName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PasswordHash")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("PhoneNumberConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("SecurityStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("TwoFactorEnabled")
//                        .HasColumnType("bit");

//                    b.Property<string>("UserName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("IdStaffNavigationIdStaff");

//                    b.ToTable("User");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserClaim", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<long>("UserId")
//                        .HasColumnType("bigint");

//                    b.HasKey("Id");

//                    b.HasIndex(new[] { "UserId" }, "IX_UserClaims_UserId");

//                    b.ToTable("UserClaim");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserLogin", b =>
//                {
//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderKey")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderDisplayName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<long>("UserId")
//                        .HasColumnType("bigint");

//                    b.HasKey("LoginProvider", "ProviderKey");

//                    b.HasIndex(new[] { "UserId" }, "IX_UserLogins_UserId");

//                    b.ToTable("UserLogin");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserToken", b =>
//                {
//                    b.Property<long>("UserId")
//                        .HasColumnType("bigint");

//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Value")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId", "LoginProvider", "Name");

//                    b.ToTable("UserToken");
//                });

//            modelBuilder.Entity("PharmacySystem.WebAPI.Models.Roles", b =>
//                {
//                    b.Property<Guid>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("ConcurrencyStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasMaxLength(250)
//                        .HasColumnType("nvarchar(250)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("NormalizedName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Roles", (string)null);

//                    b.HasData(
//                        new
//                        {
//                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
//                            ConcurrencyStamp = "a4863162-1e58-43e3-b7f8-8ebff8652525",
//                            Description = "Administrator Role",
//                            Name = "Admin",
//                            NormalizedName = "Admin"
//                        });
//                });

//            modelBuilder.Entity("PharmacySystem.WebAPI.Models.Users", b =>
//                {
//                    b.Property<Guid>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<int>("AccessFailedCount")
//                        .HasColumnType("int");

//                    b.Property<string>("ConcurrencyStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("EmailConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<long>("IdAccount")
//                        .HasColumnType("bigint");

//                    b.Property<long>("IdStaff")
//                        .HasColumnType("bigint");

//                    b.Property<bool>("LockoutEnabled")
//                        .HasColumnType("bit");

//                    b.Property<DateTimeOffset?>("LockoutEnd")
//                        .HasColumnType("datetimeoffset");

//                    b.Property<string>("NormalizedEmail")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("NormalizedUserName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PasswordHash")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("PhoneNumberConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("SecurityStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("TwoFactorEnabled")
//                        .HasColumnType("bit");

//                    b.Property<string>("UserName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Users", (string)null);

//                    b.HasData(
//                        new
//                        {
//                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
//                            AccessFailedCount = 0,
//                            ConcurrencyStamp = "6f40a10e-e10f-44be-a4bd-5521c93c656a",
//                            Email = "nguyenducnghia4112@gmail.com",
//                            EmailConfirmed = true,
//                            IdAccount = 1L,
//                            IdStaff = 1L,
//                            LockoutEnabled = false,
//                            NormalizedEmail = "nguyenducnghia4112@gmail.com",
//                            NormalizedUserName = "admin",
//                            PasswordHash = "AQAAAAEAACcQAAAAEFjXC1qdPUvvrtbyyRFzEUiHhMgF7NMfUlqlwBAhLZRhqsaMWHO4wyjaiJPb6k99EA==",
//                            PhoneNumberConfirmed = false,
//                            SecurityStamp = "",
//                            TwoFactorEnabled = false,
//                            UserName = "admin"
//                        });
//                });

//            modelBuilder.Entity("RoleUser", b =>
//                {
//                    b.Property<long>("RolesId")
//                        .HasColumnType("bigint");

//                    b.Property<long>("UsersId")
//                        .HasColumnType("bigint");

//                    b.HasKey("RolesId", "UsersId");

//                    b.HasIndex("UsersId");

//                    b.ToTable("RoleUser");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ExportInvoice", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.User", "IdAccountNavigation")
//                        .WithMany("ExportInvoices")
//                        .HasForeignKey("IdAccountNavigationId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("PharmacySystem.Models.Status", "Status")
//                        .WithMany("ExportInvoices")
//                        .HasForeignKey("StatusId")
//                        .IsRequired()
//                        .HasConstraintName("FK_ExportInvoice_Status");

//                    b.Navigation("IdAccountNavigation");

//                    b.Navigation("Status");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ImportInvoice", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.User", "IdAccountNavigation")
//                        .WithMany("ImportInvoices")
//                        .HasForeignKey("IdAccountNavigationId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("PharmacySystem.Models.Supplier", "IdSupplierNavigation")
//                        .WithMany("ImportInvoices")
//                        .HasForeignKey("IdSupplier")
//                        .IsRequired()
//                        .HasConstraintName("FK_ImportInvoice_Supplier");

//                    b.HasOne("PharmacySystem.Models.Status", "Status")
//                        .WithMany("ImportInvoices")
//                        .HasForeignKey("StatusId")
//                        .IsRequired()
//                        .HasConstraintName("FK_ImportInvoice_Status");

//                    b.Navigation("IdAccountNavigation");

//                    b.Navigation("IdSupplierNavigation");

//                    b.Navigation("Status");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.InvoiceDetail", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.ExportInvoice", "IdExportInvoiceNavigation")
//                        .WithMany("InvoiceDetails")
//                        .HasForeignKey("IdExportInvoice")
//                        .HasConstraintName("FK_InvoiceDetails_ExportInvoice");

//                    b.HasOne("PharmacySystem.Models.ImportInvoice", "IdImportInvoiceNavigation")
//                        .WithMany("InvoiceDetails")
//                        .HasForeignKey("IdImportInvoice")
//                        .HasConstraintName("FK_InvoiceDetails_ImportInvoice");

//                    b.HasOne("PharmacySystem.Models.Medicine", "IdMedicineNavigation")
//                        .WithMany("InvoiceDetails")
//                        .HasForeignKey("IdMedicine")
//                        .IsRequired()
//                        .HasConstraintName("FK_InvoiceDetails_Medicine");

//                    b.Navigation("IdExportInvoiceNavigation");

//                    b.Navigation("IdImportInvoiceNavigation");

//                    b.Navigation("IdMedicineNavigation");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Medicine", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.MedicineGroup", "IdMedicineGroupNavigation")
//                        .WithMany("Medicines")
//                        .HasForeignKey("IdMedicineGroup")
//                        .IsRequired()
//                        .HasConstraintName("FK_Medicine_MedicineGroup");

//                    b.HasOne("PharmacySystem.Models.Supplier", "IdSupplierNavigation")
//                        .WithMany("Medicines")
//                        .HasForeignKey("IdSupplier")
//                        .IsRequired()
//                        .HasConstraintName("FK_Medicine_Supplier");

//                    b.Navigation("IdMedicineGroupNavigation");

//                    b.Navigation("IdSupplierNavigation");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.RoleClaim", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.Role", "Role")
//                        .WithMany("RoleClaims")
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Role");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.staff", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.Store", "IdStoreNavigation")
//                        .WithMany("staff")
//                        .HasForeignKey("IdStore")
//                        .HasConstraintName("FK_Staff_Store");

//                    b.Navigation("IdStoreNavigation");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Supplier", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.SupplierGroup", "IdSupplierGroupNavigation")
//                        .WithMany("Suppliers")
//                        .HasForeignKey("IdSupplierGroup")
//                        .IsRequired()
//                        .HasConstraintName("FK_Supplier_SupplierGroup");

//                    b.Navigation("IdSupplierGroupNavigation");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.User", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.staff", "IdStaffNavigation")
//                        .WithMany("Users")
//                        .HasForeignKey("IdStaffNavigationIdStaff")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("IdStaffNavigation");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserClaim", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.User", "User")
//                        .WithMany("UserClaims")
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserLogin", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.User", "User")
//                        .WithMany("UserLogins")
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.UserToken", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.User", "User")
//                        .WithMany("UserTokens")
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });

//            modelBuilder.Entity("RoleUser", b =>
//                {
//                    b.HasOne("PharmacySystem.Models.Role", null)
//                        .WithMany()
//                        .HasForeignKey("RolesId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("PharmacySystem.Models.User", null)
//                        .WithMany()
//                        .HasForeignKey("UsersId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ExportInvoice", b =>
//                {
//                    b.Navigation("InvoiceDetails");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.ImportInvoice", b =>
//                {
//                    b.Navigation("InvoiceDetails");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Medicine", b =>
//                {
//                    b.Navigation("InvoiceDetails");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.MedicineGroup", b =>
//                {
//                    b.Navigation("Medicines");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Role", b =>
//                {
//                    b.Navigation("RoleClaims");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.staff", b =>
//                {
//                    b.Navigation("Users");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Status", b =>
//                {
//                    b.Navigation("ExportInvoices");

//                    b.Navigation("ImportInvoices");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Store", b =>
//                {
//                    b.Navigation("staff");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.Supplier", b =>
//                {
//                    b.Navigation("ImportInvoices");

//                    b.Navigation("Medicines");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.SupplierGroup", b =>
//                {
//                    b.Navigation("Suppliers");
//                });

//            modelBuilder.Entity("PharmacySystem.Models.User", b =>
//                {
//                    b.Navigation("ExportInvoices");

//                    b.Navigation("ImportInvoices");

//                    b.Navigation("UserClaims");

//                    b.Navigation("UserLogins");

//                    b.Navigation("UserTokens");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
