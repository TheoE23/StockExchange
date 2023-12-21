using Microsoft.Extensions.Configuration;

using StockExchange.Gateway.Options;
using StockExchange.Gateway.Services.HttpRequests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .Configure<EndpointsOptions>(
        builder.Configuration.GetSection(
            nameof(EndpointsOptions)));

builder.Services.AddControllers();

builder.Services.AddTransient<IHttpRequestService, HttpRequestService>();

builder.Services.AddHttpClient<HttpRequestService>();

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
