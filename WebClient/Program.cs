using Microsoft.AspNetCore.Authentication.Cookies;
using WebClient.Hubs;
using WebClient.SubscribeTableDependencies;
using WebClient.MiddlewareExtensions;
using WebClient.Interface;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using BAL.Profiles;

var builder = WebApplication.CreateBuilder(args);

// SignalR
var connectionString = builder.Configuration.GetConnectionString("DBStore");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSignalR();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();
builder.Services.AddSingleton<IUserConnectionManager, UserConnectionManager>();

builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddSingleton<INotificationDAO, NotificationDAO>();
builder.Services.AddAutoMapper(typeof(NotificationProfile));
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews(); 

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

var app = builder.Build();



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
app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);

app.Run();
