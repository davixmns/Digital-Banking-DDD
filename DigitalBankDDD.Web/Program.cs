using DigitalBankDDD.Infra.Database;
using DigitalBankDDD.Infra.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BankContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

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

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();
DatabaseConnectionTester.TestConnection(dbContext);

app.Run();