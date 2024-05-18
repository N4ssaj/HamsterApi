using HamsterApi.Api.Configurate;
using HamsterApi.Application.DIExtensions;
using HamsterApi.Persistence.DIExtension;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dir = builder.Configuration.GetConnectionString("Dir");

builder.Services.AddPersistence(connectionString, dir);
builder.Services.AddApplication();
builder.Services.AddLogger();
builder.Services.AddRedis();
builder.Services.RegisterService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Default", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Seq("http://hamster-seq:5341")
    .WriteTo.Console();
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors(builder=>builder.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
