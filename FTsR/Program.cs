using FTsR;
using FTsR.Hubs;
using FTsR.MiddlewareExtensions;
using FTsR.SubscribeTableDependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FTsR.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using FTsR.Models;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DataDbContext' not found.")));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<CompaniesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<MessagesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<PinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddSignalR();

builder.Services.AddIdentity<UserModel, IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<DataHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
//builder.Services.AddSingleton<SubscribeCompaniesTableDependency>();
builder.Services.AddSingleton<SubscribePinsTableDependency>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 
app.MapHub<DashboardHub>("/dashboardHub");
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<DataHub>("/dataHub");
app.MapHub<ChatHub>("/chatHub");

app.UseProductTableDependency();
//app.UseCompaniesTableDependency();
app.UsePinsTableDependency();

app.Run();

