using bank.Models;
using bank;
using Microsoft.EntityFrameworkCore;
using bank.Models.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    // Configure session options as needed
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/MessageInfo/Index"; // Specify your login path
    options.AccessDeniedPath = "/UserInformation/AccessDenied"; // Specify your access denied path if needed
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserInformation, UserInformation>();

builder.Services.AddScoped<IGroupName, GroupName>();

builder.Services.AddScoped<IUserGroup, UserGroup>();

builder.Services.AddScoped<IMessageInfo, MessageInfo>();

builder.Services.AddScoped<IParty, Party>();

builder.Services.AddScoped<IDealer, Dealer>();

builder.Services.AddScoped<ICurrency, Currency>();

builder.Services.AddScoped<IBankDetails, BankDetails>();

builder.Services.AddScoped<IForexButSellDeals, ForexButSellDeals>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserInformation}/{action=Index}/{id?}");

app.Run();
