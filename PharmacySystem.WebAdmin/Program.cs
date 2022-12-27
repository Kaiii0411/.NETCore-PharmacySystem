using PharmacySystem.APIIntergration;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    //EnvironmentName = Environments.Staging,
    WebRootPath = "wwwroot"
});


builder.Services.AddHttpClient();
builder.Services.AddTransient<IMedicineApiClient, MedicineApiClient>();
builder.Services.AddTransient<IMedicineGroupApiClient, MedicineGroupApiClient>();
builder.Services.AddTransient<ISupplierApiClient, SupplierApiClient>();
builder.Services.AddTransient<ISupplierGroupApiClient, SupplierGroupApiClient>();
builder.Services.AddTransient<IInvoiceApiClient, InvoiceApiClient>();
builder.Services.AddTransient<IStaffApiClient, StaffApiClient>();
builder.Services.AddTransient<IStoreApiClient, StoreApiClient>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
