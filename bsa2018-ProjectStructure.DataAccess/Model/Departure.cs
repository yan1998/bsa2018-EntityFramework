using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Departure:Entity
    {
        public DateTime DepartureTime { get; set; }

        [Required]
        public int IdFlight { get; set; }
        public Flight Flight { get; set; }

        [Required]
        public int IdCrew { get; set; }
        public Crew Crew { get; set; }

        [Required]
        public int IdAircraft { get; set; }
        public Aircraft Aircraft { get; set; }
    }
}
