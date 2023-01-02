using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models;
using PharmacySystem.Models.Identity;
using PharmacySystem.Models.Validation;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    //EnvironmentName = Environments.Staging,
    WebRootPath = "wwwroot"
});

builder.Services.AddDbContext<PharmacySystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PharmacySystemDB")));

builder.Services.AddDefaultIdentity<Users>()
                    .AddRoles<Roles>()
                    .AddEntityFrameworkStores<PharmacySystemContext>().AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/User/Forbidden";
    });

builder.Services.AddTransient<IMedicineApiClient, MedicineApiClient>();
builder.Services.AddTransient<IMedicineGroupApiClient, MedicineGroupApiClient>();
builder.Services.AddTransient<ISupplierApiClient, SupplierApiClient>();
builder.Services.AddTransient<ISupplierGroupApiClient, SupplierGroupApiClient>();
builder.Services.AddTransient<IInvoiceApiClient, InvoiceApiClient>();
builder.Services.AddTransient<IStaffApiClient, StaffApiClient>();
builder.Services.AddTransient<IStoreApiClient, StoreApiClient>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IRolesApiClient, RolesApiClient>();

builder.Services.AddControllersWithViews()
         .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.



var app = builder.Build();


//app.Logger.LogInformation("ASPNETCORE_ENVIRONMENT: {env}",
//      Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

//app.Logger.LogInformation("app.Environment.IsDevelopment(): {env}",
//      app.Environment.IsDevelopment().ToString());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
