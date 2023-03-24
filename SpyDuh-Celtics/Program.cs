using SpyDuh_Celtics.Repositories;

namespace SpyDuh_Celtics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //creating web app

            // Add services to the container.

            builder.Services.AddTransient<IServicesRepository, ServicesRepository>(); //depedency injection
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); //buidling app

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); //mapping controllers

            app.Run(); //Listening for requests
        }
    }
}