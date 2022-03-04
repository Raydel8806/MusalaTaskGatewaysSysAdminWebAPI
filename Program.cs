using GatewaysSysAdminWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Use local MSSQL Server "GatewaysDB" or in cloud "CloudGatewaysDB"
/// </summary>
builder.Services
    .AddDbContextFactory<GatewaysSysAdminDBContext>(optionsAction => optionsAction
    .UseSqlServer(configuration
    .GetConnectionString("GatewaysDB"))); 

/// <summary>
/// Use SQLGatewayRepository or other GatewayRepository technology. Ex. MockGatewayRepository
/// Comment using // to use MockGatewayRepository
/// </summary>
 builder.Services.AddTransient<IGatewayRepository,SQLGatewayRepository>();

/// <summary>
/// Uncoment to use MockGatewayRepository
/// </summary>
//builder.Services.AddTransient<IGatewayRepository, MockGatewayRepository>();

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
