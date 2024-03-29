﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace jwLogbook.models
{
    public class FlightModel
    {
        [Key]
        public string FlightID { get; set; }
        public string UserID { get; set; }
        public string AircraftID { get; set; }

        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Duration { get; set; }

        [AllowNull]
        public decimal PIC { get; set; }
        [AllowNull]
        public decimal Night { get; set; }

        [AllowNull]
        public int LandingsDay { get; set; }
        [AllowNull]
        public int LandingsNight { get; set; }
        public int Landings { get; set; }



        [ForeignKey("UserID")]
        public UserModel User { get; set; }

        [ForeignKey("AircraftID")]
        public AircraftModel Aircraft { get; set; }
    }
}
