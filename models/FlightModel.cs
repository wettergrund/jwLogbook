using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwLogbook.models
{
    public class FlightModel
    {
        [Key]
        public int FlightID { get; set; }
        public int UserID { get; set; }
        public int AircraftID { get; set; }

        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Duration { get; set; }

        [ForeignKey("UserID")]
        public UserModel User { get; set; }

        [ForeignKey("AircraftID")]
        public AircraftModel Aircraft { get; set; }
    }
}
