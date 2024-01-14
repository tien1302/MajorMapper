using BAL.Services.Implementations;
using BAL.Services.Interfaces;
using BAL.DTOs.Authentications;
using BAL.Profiles;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT 
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Major Mapper Application API",
        Description = "JWT Authentication API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Key"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

//add CORS
builder.Services.AddCors(cors => cors.AddPolicy(
                                        name: "WebPolicy",
                                        build =>
                                        {
                                            build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                        }
                                    ));

builder.Services.Configure<JwtAuth>(builder.Configuration.GetSection("JwtAuth"));
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPersonalityTypeRepository, PersonalityTypeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<IMethodRepository, MethodRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestQuestionRepository, TestQuestionRepository>();

builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPersonalityTypeService, PersonalityTypeService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<IMethodService, MethodService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITestResultService, TestResultService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestQuestionService, TestQuestionService>();

builder.Services.AddAutoMapper(typeof(AccountProfile),
                               typeof(BookingProfile),
                               typeof(NotificationProfile),
                               typeof(RoleProfile),
                               typeof(MajorProfile),
                               typeof(PersonalityTypeProfile),
                               typeof(SlotProfile),
                               typeof(PaymentProfile),
                               typeof(TestResultProfile),
                               typeof(ScoreProfile),
                               typeof(QuestionProfile),
                               typeof(TestProfile),
                               typeof(MethodProfile),
                               typeof(TestQuestionProfile));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}
app.UseCors("WebPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
