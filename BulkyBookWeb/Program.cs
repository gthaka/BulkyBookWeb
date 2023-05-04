using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

bool isInDockerContainer = (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true");
string connStr = isInDockerContainer ? "DOCKERconnstr" : "DefaultConnection";

// Obtain configuration settings
var connection = builder.Configuration.GetConnectionString(connStr);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnection")
//    ));

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
