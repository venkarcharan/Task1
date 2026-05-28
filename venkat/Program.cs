using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;

using System.Text;

using venkat.service.Abstraction;
using venkat.service.Implementation;

using venkat.store.Abstraction;
using venkat.store.Implementation;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// SWAGGER + JWT AUTHORIZATION

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "venkat API",
            Version = "v1"
        });

    // JWT SECURITY

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter JWT token like: Bearer YOUR_TOKEN"
        });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                new string[] {}
            }
        });
});

// JWT AUTHENTICATION

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });

builder.Services.AddAuthorization();

// DEPENDENCY INJECTION

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<IMovieStore, MovieStore>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAuthStore, AuthStore>();

var app = builder.Build();

// MIDDLEWARE

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();