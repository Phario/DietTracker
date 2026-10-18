using System.Reflection;
using DietTracker.Core;
using DietTracker.Extensions;
using DietTracker.Persistence;
using DietTracker.Persistence.Domain;
using DietTracker.Persistence.Domain.Users;
using DietTracker.Persistence.Seeding;
using Scalar.AspNetCore;
using DispatchR.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDispatchR(options =>
    options.Assemblies.Add(Assembly.Load(builder.Configuration["DispatchR:CoreAssembly"])));

builder.Services.AddOpenApi();

builder.Services.AddCoreServices(builder.Configuration);
var app = builder.Build();

app.MapEndpoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.MapGet("/", () => Results.Redirect("/scalar"))
        .ExcludeFromDescription();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
    
    var context = scope.ServiceProvider.GetRequiredService<DietTrackerDbContext>();
    await context.Database.MigrateAsync();
    await ApplicationSeeder.SeedAsync(context, userManager, roleManager);
}

app.UseHttpsRedirection();


app.Run();