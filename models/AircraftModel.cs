using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwLogbook.models
{
    public class AircraftModel
    {
        [Key]
        public int AcID { get; set; }

        [MaxLength(25)]
        [Required]
        public string Registration { get; set; }

        [MaxLength(50)]
        [Required]
        public string Model { get; set; }

        [MaxLength(5)]
        [Required]
        public string ICAOType { get; set; }

    }
}
