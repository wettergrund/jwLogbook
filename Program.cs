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
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors();

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

            app.MapGet("API/logbyuser", async (int id, int? number) =>
            {
                LogbookDbContext context = new LogbookDbContext();

                var log = context.FlightLogs.Where(u => u.UserID == id).ToList();
                if (number.HasValue)
                {
                    var sliced = log.OrderBy(i => i.DepartureTime).TakeLast(number.Value);
                    return Results.Ok(sliced);

                }
                else
                {
                    return Results.Ok(log);

                }
            });

            app.MapPut("API/addaircraft", async (string reg, string model, string type) =>
            {
                LogbookDbContext context = new LogbookDbContext();

                AircraftModel newAc = new AircraftModel() {
                    Registration = reg,
                    Model = model,
                    ICAOType = type
                };

                context.Add(newAc);
                context.SaveChanges();


                var aircraft = context.Aircraft.ToList();

                return Results.Ok(aircraft);
            });

            app.MapPut("API/addlog", async (int userId, int acId, string depAp, string arrAp, DateTime depUTC, DateTime arrUTC) =>
            {
                LogbookDbContext context = new LogbookDbContext();

                FlightModel newFlight = new FlightModel()
                {
                    UserID = userId,
                    AircraftID = acId,
                    DepartureAirport = depAp,
                    ArrivalAirport = arrAp,
                    DepartureTime= depUTC,
                    ArrivalTime= arrUTC
                };

                TimeSpan duration = arrUTC - depUTC;
                decimal decDur = Math.Round((decimal)duration.TotalHours,2);

                newFlight.Duration = decDur;

                context.Add(newFlight);
                context.SaveChanges();


                var logs = context.FlightLogs.ToList();

                return Results.Ok(logs);


            });



                app.Run();
        }
    }
}