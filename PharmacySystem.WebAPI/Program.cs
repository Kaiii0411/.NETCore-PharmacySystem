using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PharmacySystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PharmacySystemDB")));


//Service
#region Services
builder.Services.AddHttpClient();
builder.Services.AddTransient<IMedicineService, MedicineService>();
builder.Services.AddTransient<IMedicineGroupService, MedicineGroupService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<ISupplierGroupService, SupplierGroupService>();
builder.Services.AddTransient<IStaffService, StaffService>();
builder.Services.AddTransient<IInvoiceService, InvoiceService>();
builder.Services.AddTransient<IInvoiceDetailsService, InvoiceDetailsService>();
#endregion

//Repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IMedicineRepo, MedicineRepo>();
builder.Services.AddTransient<IMedicineGroupRepo, MedicineGroupRepo>();
builder.Services.AddTransient<ISupplierRepo, SupplierRepo>();
builder.Services.AddTransient<ISupplierGroupRepo, SupplierGroupRepo>();
builder.Services.AddTransient<IStoreRepo, StoreRepo>();
builder.Services.AddTransient<IStaffRepo, StaffRepo>();

builder.Services.AddTransient<IImportInvoiceRepo, ImportInvoiceRepo>();
builder.Services.AddTransient<IExportInvoiceRepo, ExportInvoiceRepo>();



builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
