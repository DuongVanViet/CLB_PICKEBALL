using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PcmBackend.Data;
using PcmBackend.Hubs;
using PcmBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Listen on PORT from environment (Render sets this to 8080), default to 5000 for local dev
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// ==================== Database Configuration ====================
// Use PostgreSQL for production (on Render), SQLite for local development
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (isDevelopment || string.IsNullOrEmpty(connString) || !connString.Contains("Host="))
    {
        // Local development: use SQLite
        var sqliteConn = connString ?? "Data Source=PcmBackend.db";
        options.UseSqlite(sqliteConn);
    }
    else
    {
        // Production: use PostgreSQL (Render)
        options.UseNpgsql(connString, npgOptions =>
        {
            npgOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelaySeconds: 10, deltaSeconds: 1);
        });
    }
});

// ==================== Identity Configuration ====================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ==================== JWT Configuration ====================
// IMPORTANT: Override JWT secrets in production via environment variables
var jwtKey = builder.Configuration["Jwt:Key"] ?? 
    Environment.GetEnvironmentVariable("JWT_KEY") ?? 
    "YourDefaultSecretKey123456789012345678901234";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? 
    Environment.GetEnvironmentVariable("JWT_ISSUER") ?? 
    "PcmBackend";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? 
    Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? 
    "PcmMobileApp";

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
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    // Support SignalR authentication via query string
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/pcm"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// ==================== CORS Configuration ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    options.AddPolicy("AllowMobileApp", policy =>
    {
        var allowedOrigins = new[]
        {
            "http://localhost:3000",
            "https://localhost:3000",
            "https://clb-pickeball.onrender.com" // Render frontend URL
        };
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ==================== Controllers & SignalR ====================
builder.Services.AddControllers();
builder.Services.AddSignalR();

// ==================== Background Services ====================
builder.Services.AddHostedService<AutoCancelUnpaidBookingsService>();
builder.Services.AddHostedService<AutoRemindService>();

// ==================== Swagger Configuration ====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pickleball Club Management API",
        Version = "v1",
        Description = "API cho hệ thống quản lý CLB Pickleball - Vợt Thủ Phố Núi"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ==================== Seed Database ====================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Apply migrations
        context.Database.Migrate();

        // Seed data
        await DbSeeder.SeedAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// ==================== Middleware Pipeline ====================
// Enable Swagger in all environments for testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PCM API v1");
    c.RoutePrefix = "swagger";
});

// app.UseHttpsRedirection(); // Disabled for development - allow HTTP

// Handle CORS preflight OPTIONS requests
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PcmHub>("/hubs/pcm");

app.Run();
