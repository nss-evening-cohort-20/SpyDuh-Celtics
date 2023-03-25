using SpyDuh_Celtics.Repositories;
using SpyDuh_Celtics.Repository;

namespace SpyDuh_Celtics
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<ISkillsRepository, SkillsRepository>();


            // Add services to the container.

            builder.Services.AddTransient<IRelationshipRepository, RelationshipRepository>();
            builder.Services.AddTransient<IServicesRepository, ServicesRepository>(); //depedency injection
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); //buidling app

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); //Listening for requests
        }
    }
}