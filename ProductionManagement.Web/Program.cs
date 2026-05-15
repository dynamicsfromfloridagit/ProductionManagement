using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductionManagement.Web.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    );

builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("BlobConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


var stringAdress = new StringBuilder();
// TODO: build api address from configuration
//var baseAddressUri = new Uri(builder.Configuration.GetValue<string>("ParticleApiBaseAddress") ?? throw new InvalidOperationException("Configuration value 'ParticleApiBaseAddress' not found."));
stringAdress.Append("https://api.particle.io/v1/devices/");
stringAdress.Append(builder.Configuration.GetValue<string>("ParticleDeviceId") ?? throw new InvalidOperationException("Configuration value 'ParticleDeviceId' not found."));
stringAdress.Append("/");

var stringBearer = builder.Configuration.GetValue<string>("BoronBearer") ?? throw new InvalidOperationException("config bearer boron value 'BoronBearer' not found");

builder.Services.AddHttpClient("DefaultHttpClient", client => {
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", stringBearer);
    client.BaseAddress = new Uri(stringAdress.ToString());
   // client.BaseAddress = baseAddressUri;
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Applies pending migrations and creates the database if it doesn't exist
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
