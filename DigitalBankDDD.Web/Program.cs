using System.Reflection;
using DigitalBankDDD.Application.Commands;
using DigitalBankDDD.Application.Handlers;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Mapper;
using DigitalBankDDD.Application.PipelineBehaviors;
using DigitalBankDDD.Application.Services;
using DigitalBankDDD.Application.Validators;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Domain.Services;
using DigitalBankDDD.Infra.Context;
using DigitalBankDDD.Infra.Repositories;
using DigitalBankDDD.Infra.Utils;
using DigitalBankDDD.Web.Handlers;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

//Infraestructure
builder.Services.AddDbContext<BankContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Application
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Domain
builder.Services.AddScoped<IAccountDomainService, AccountDomainService>();

//Fluent Validation
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTransactionCommand>());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<AmountValidator>();
builder.Services.AddFluentValidationAutoValidation(); //validação seja feita automaticamente
builder.Services.AddFluentValidationClientsideAdapters(); //Mensagens de erro de validação sejam exibidas no front-end

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();

DatabaseConnectionTester.TestConnection(dbContext);

app.MapControllers();

app.Run();