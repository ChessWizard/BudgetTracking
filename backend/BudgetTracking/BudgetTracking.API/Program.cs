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
using BudgetTracking.Service.Services.User.RegisterUser;
using BudgetTracking.Data.Configurations;
using BudgetTracking.Service.Validators.User;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();

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

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    var tokenOptions = builder.Configuration.GetSection("TokenOptionConfigurations").Get<TokenOptionConfigurations>();
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidAudience = tokenOptions.Audiences[0],
//        ValidIssuer = tokenOptions.Issuer,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = SignTokenHelper.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
//        ClockSkew = TimeSpan.Zero,
//        ValidateLifetime = true
//    };
//});

builder.Services.Configure<TokenOptionConfigurations>(builder.Configuration.GetSection("TokenOptionConfigurations"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();