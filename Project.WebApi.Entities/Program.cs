using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("Context");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connection));

var app = builder.Build();
app.Run();
