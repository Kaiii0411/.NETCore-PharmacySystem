using Microsoft.EntityFrameworkCore;
using PharmacySystem.Models;
using PharmacySystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PharmacySystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PharmacySystemDB")));

builder.Services.AddTransient<IMedicineService, MedicineService>();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
