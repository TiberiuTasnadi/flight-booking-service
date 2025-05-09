// <copyright file="Program.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Reflection;
using FlightBooking.Api.Middleware;
using FlightBooking.Api.Registration;
using FlightBooking.Application.Adapter.Registration;
using FlightBooking.Application.Registration;
using FlightBooking.Persistance.DbContexts;
using FlightBooking.Persistance.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FlightBooking.Api;

/// <summary>
/// Main entry point for the web application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method to configure and run the web application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Flight Booking API",
                Version = "v1",
                Description = "API for flight search and booking management.\nDeveloped for a technical test using Clean Architecture and SOLID principles.",
                Contact = new OpenApiContact
                {
                    Name = "Tibi",
                    Email = "ttasnadi.olivera@gmail.com"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddApplicationServices();
        builder.Services.AddMappers();
        builder.Services.AddAdapters();
        builder.Services.AddValidators();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddExternalApiServices(builder.Configuration);

        SetupDatabase(builder);

        var app = builder.Build();

        MigrateDatabase(app);

        app.UseMiddleware<ExceptionHandlingMiddleware>();

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
    }

    private static void SetupDatabase(WebApplicationBuilder builder)
    {
        var dbPath = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(dbPath))
        {
            throw new InvalidOperationException("Database connection string is not configured.");
        }

        if (dbPath.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
        {
            var rawPath = dbPath.Replace("Data Source=", string.Empty, StringComparison.OrdinalIgnoreCase).Trim();

            var directory = Path.GetDirectoryName(rawPath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }

    private static void MigrateDatabase(WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}