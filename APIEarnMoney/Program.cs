
using APIEarnMoney.Helpers;
using APIEarnMoney.Models.Databases;
using APIEarnMoney.Services.Implements;
using APIEarnMoney.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEarnMoney
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
            builder.Services.AddScoped<IEarnMoneyService, EarnMoneyService>();
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));
            var app = builder.Build();
            var dbInit = new DatabaseInitializer(app.Services);
            dbInit.Initialize();
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
