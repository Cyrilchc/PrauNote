using Auth.Models;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context
builder.Services.AddSingleton<Context>();

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
            //ClockSkew = TimeSpan.Zero // Sert à enlever le décalage de temps sur les expirations courtes : https://stackoverflow.com/a/67677801/10506880
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapGet("/appointments", async (Context db) =>
    await db.Appointments.ToListAsync()).RequireAuthorization();

// Points de terminaison permettant de récupérer le jeton / de créer un compte à ajouter

app.Run();