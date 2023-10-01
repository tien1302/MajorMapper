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

builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IPersonalityTypeRepository, PersonalityTypeRepository>();
builder.Services.AddScoped<IPersonalityTypeDAO, PersonalityTypeDAO>();
builder.Services.AddScoped<IMajorDAO, MajorDAO>();

builder.Services.AddAutoMapper(typeof(MajorProfile),
                               typeof(PersonalityTypeProfile));
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
