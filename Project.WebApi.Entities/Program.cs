using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;

var builder = WebApplication.CreateBuilder(args);

var conStrBuilder = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("Context"));
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();
app.Run();
