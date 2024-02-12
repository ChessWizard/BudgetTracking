using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.UnitofWork;
using BudgetTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using BudgetTracking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BudgetTracking.Service.Services.Token.Commands.CreateToken;
using BudgetTracking.Data.Repositories;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Data.Configurations;
using BudgetTracking.Service.Validators.User;
using FluentValidation;
using BudgetTracking.Service.Services.User.Commands.RegisterUser;
using BudgetTracking.Data.ContextAccessor;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Data.Helpers;
using Microsoft.OpenApi.Models;
using BudgetTracking.API.Extensions;
using BudgetTracking.Core.File;
using BudgetTracking.Data.File;
using BudgetTracking.Service.Services.File.Commands.ExportFile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Budget Tracking API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}); ;

// UTC Problemlerini PostgreSQL için
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPaymentAccountRepository, PaymentAccountRepository>();
builder.Services.AddScoped<ISecurityContextAccessor, SecurityContextAccessor>();
builder.Services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IExcelService, ExcelService>();

//builder.Services.Configure<TokenOptionConfigurations>(builder.Configuration.GetSection("TokenOptionConfigurations"));

// EF Core entegrasyonu
builder.Services.AddDbContext<BudgetTrackingDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"), dbOptions =>
    {
        dbOptions.MigrationsAssembly("BudgetTracking.Data");
    });
});

// Identity ile EF Core entegrasyonu
builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<BudgetTrackingDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptionConfigurations").Get<TokenOptionConfigurations>();
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = tokenOptions.Audiences[0],
        ValidIssuer = tokenOptions.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SignTokenHelper.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true
    };
});

builder.Services.Configure<TokenOptionConfigurations>(builder.Configuration.GetSection("TokenOptionConfigurations"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                                              policy.AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials()
                                                    .SetIsOriginAllowed(origin => true)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handler
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();