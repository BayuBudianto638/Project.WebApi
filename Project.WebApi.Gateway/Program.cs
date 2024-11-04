using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Project.WebApi.Gateway.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Information);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<SerilogMidware>();

app.UseSwaggerForOcelotUI(option =>
{
    //option.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseOcelot().Wait();

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
