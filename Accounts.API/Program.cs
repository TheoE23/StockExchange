using Accounts.Infrastructure.Repositories;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.DatabaseConnections;
using Accounts.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Accounts.Domain.Abstraction.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

var connectionString = "Server=localhost;Database=StockAccountSystem;User Id=sa; Password=sqldocker2022;";
builder.Services.AddScoped<DatabaseConnection>(_ => new DatabaseConnection(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();
builder.Services.AddScoped<IPasswordHashing, PasswordHashing>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthenticationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
