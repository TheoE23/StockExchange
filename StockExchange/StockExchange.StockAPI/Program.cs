using StockAPI.Configuration;
using StockAPI.Models;
using StockAPI.Data;
using StockAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace StockAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddHttpClient<IStockAPIService, StockAPIService>();
            builder.Services.AddScoped<IStockAPIService, StockAPIService>();

            //Adding the API Key Service
            builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ApiConfiguration"));
            builder.Services.AddTransient(provider => provider.GetRequiredService<IOptions<ApiConfiguration>>().Value.ApiKey);
            //db service
            builder.Services.AddSingleton(new SqliteHelper(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        }
    }
}
