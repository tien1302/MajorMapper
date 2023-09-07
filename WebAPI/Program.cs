using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.Profiles;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IMajorDAO, MajorDAO>();
builder.Services.AddScoped<IUniversityDAO, UniversityDAO>();
builder.Services.AddScoped<IAccountDAO, AccountDAO>();
builder.Services.AddScoped<IRoleDAO, RoleDAO>();
builder.Services.AddScoped<IBookingDAO, BookingDAO>();
builder.Services.AddScoped<INotificationDAO, NotificationDAO>();

builder.Services.AddAutoMapper(typeof(UniversityProfile),
                               typeof(MajorProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
