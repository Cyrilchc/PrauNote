using System.Linq;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Voir le fichier html dans le projet pour un tuto minimal api

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("JWT Token", new OpenApiSecurityScheme()
    {
        Description = "JWT Token",
        Name = "Authorization",
        In = ParameterLocation.Header
    });
});

// Add database context
builder.Services.AddTransient<Context>();

// Auth config
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Api:Issuer"], // https://stackoverflow.com/a/69722959
            ValidAudience = builder.Configuration["Api:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Api:Key"])),
            //ClockSkew = TimeSpan.Zero // Sert � enlever le d�calage de temps sur les expirations courtes : https://stackoverflow.com/a/67677801/10506880
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        //.AllowCredentials()
        .Build());
});

var app = builder.Build();

// Apply database migrations at runtime
Context context = app.Services.GetRequiredService<Context>();
if (context.Database.GetPendingMigrations().Any())
    context.Database.Migrate();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// All contr�lers require the request to be authorized by default
//app.MapControllers().RequireAuthorization();

app.Run();
