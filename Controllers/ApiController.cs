using jwLogbook.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwLogbook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly LogbookDbContext _context;
        public ApiController(LogbookDbContext context)
        {
            _context = context;
        }

        [HttpGet("all/users")]
        public IActionResult Index()
        {
            var users = _context.Pilots.ToList();

            return Ok(users);

        }

        [HttpGet("all/aircraft")]
        public IActionResult Ac()
        {
            var aircraft = _context.Aircraft.ToList();

            return Ok(aircraft);

        }

        [HttpGet("userlog")]
        public IActionResult Logs(string id, int? number)
        {

            var log = _context.FlightLogs.Where(u => u.UserID == id).ToList();
            if (number.HasValue)
            {
                var sliced = log.OrderBy(i => i.DepartureTime).TakeLast(number.Value);
                return Ok(sliced);

            }
            else
            {
                return Ok(log);

            }

        }

        [HttpPost("aircraft")]
        public IActionResult PostAc(string reg, string model, string type)
        {
            AircraftModel newAc = new AircraftModel()
            {
                AcID = Guid.NewGuid().ToString(),
                Registration = reg,
                Model = model,
                ICAOType = type
            };

            _context.Add(newAc);
            _context.SaveChanges();


            var aircraft = _context.Aircraft.ToList();

            return Ok(aircraft);

        }

        [HttpPost("log")]
        public IActionResult PostLog(string userId, string acId, string depAp, string arrAp, DateTime depUTC, DateTime arrUTC)
        {
            FlightModel newFlight = new FlightModel()
            {
                UserID = userId,
                AircraftID = acId,
                DepartureAirport = depAp,
                ArrivalAirport = arrAp,
                DepartureTime = depUTC,
                ArrivalTime = arrUTC
            };

            TimeSpan duration = arrUTC - depUTC;
            decimal decDur = Math.Round((decimal)duration.TotalHours, 2);

            newFlight.Duration = decDur;

            _context.Add(newFlight);
            _context.SaveChanges();


            var logs = _context.FlightLogs.ToList();

            return Ok(logs);

        }

    }
}
