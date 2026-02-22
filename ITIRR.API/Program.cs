using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using ITIRR.Infrastructure.Repositories;
using ITIRR.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Allow Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Register Logging Service
builder.Services.AddScoped<ILogService, LogService>();

// Register Repositories
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBoatListingService, BoatListingService>();
builder.Services.AddScoped<IJetListingService, JetListingService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCategoryRepository, VehicleCategoryRepository>();
builder.Services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleListingRepository, VehicleListingRepository>();
builder.Services.AddScoped<IVehicleListingService, VehicleListingService>();
builder.Services.AddScoped<IFileUploadService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new FileUploadService(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
});

// Register Services
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleCategoryService, VehicleCategoryService>();
builder.Services.AddScoped<IVehicleBrandService, VehicleBrandService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ITIRR API",
        Version = "v1",
        Description = "ITIRR Luxury Vehicle Rental API"
    });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();