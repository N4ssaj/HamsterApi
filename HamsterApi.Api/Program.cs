using AutoMapper;
using HamsterApi.Api.Configurate;
using HamsterApi.Application.Service;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess;
using HamsterApi.DataAccess.MappingProfile;
using HamsterApi.DataAccess.Repositories;
using VDS.RDF.Query.Algebra;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Register(connectionString!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
