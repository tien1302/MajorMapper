using DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebClient.Hubs;
using WebClient.MiddlewareExtensions;
using WebClient.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);
// DB Context
var connectionString = builder.Configuration.GetConnectionString("DBStore");
builder.Services.AddDbContext<MajorMapperContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/account/google-login";
})
.AddGoogle(options =>
{
    options.ClientId = "33202222454-l4peaj7en9lg693sf1nc179vinqdv16t.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
builder.Services.AddSignalR();
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


app.UseSession();
app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");
app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
