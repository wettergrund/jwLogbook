global using jwLogbook.connects;
using jwLogbook.models;

namespace jwLogbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

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

            app.MapGet("API/users", async () =>
            {
                LogbookDbContext context = new LogbookDbContext();

                var users = context.Pilots.ToList();

                return Results.Ok(users);
            });

            app.MapGet("API/aircraft", async () =>
            {
                LogbookDbContext context = new LogbookDbContext();

                var aircraft = context.Aircraft.ToList();

                return Results.Ok(aircraft);
            });



            app.Run();
        }
    }
}