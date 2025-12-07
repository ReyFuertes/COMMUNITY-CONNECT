using CommunicationHub.Infrastructure.Persistence;
using CommunicationHub.Domain.Interfaces;
using CommunicationHub.Infrastructure.Repositories;
using CommunicationHub.Application;
using CommunicationHub.Application.Interfaces;
using CommunicationHub.Api.Hubs;
using CommunicationHub.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CommunicationHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<INotificationService, SignalRNotificationService>();

builder.Services.AddSignalR();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
      new string[] { }
    }
  });

});

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// Ensure secret is not null, fallback for dev if needed
var secretStr = builder.Configuration["JwtSettings:Secret"] ?? "a-very-long-and-super-secret-key-for-development";
var secretKey = Encoding.ASCII.GetBytes(secretStr);

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false, // Set to true in prod with valid issuer
    ValidateAudience = false, // Set to true in prod with valid audience
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtSettings["Issuer"],
    ValidAudience = jwtSettings["Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
  };

  // Configure JWT Bearer to read token from query string for SignalR
  options.Events = new JwtBearerEvents
  {
      OnMessageReceived = context =>
      {
          var accessToken = context.Request.Query["access_token"];

          // If the request is for our hub...
          var path = context.HttpContext.Request.Path;
          if (!string.IsNullOrEmpty(accessToken) &&
              (path.StartsWithSegments("/hubs/communication")))
          {
              // Read the token out of the query string
              context.Token = accessToken;
          }
          return Task.CompletedTask;
      }
  };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var logger = services.GetRequiredService<ILogger<Program>>();
  try 
  {
      var context = services.GetRequiredService<CommunicationHubDbContext>();
      context.Database.Migrate();
  }
  catch (Exception ex)
  {
      logger.LogError(ex, "An error occurred while migrating the database.");
  }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapHub<CommunityHub>("/hubs/communication");

app.Run();