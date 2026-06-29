using System.Reflection;
using DietTracker.Core;
using DietTracker.Extensions;
using DietTracker.Persistence;
using Scalar.AspNetCore;
using DispatchR.Extensions;
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
    var context = scope.ServiceProvider.GetRequiredService<DietTrackerDbContext>();
    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();


app.Run();