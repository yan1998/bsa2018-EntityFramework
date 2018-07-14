using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Flight : Entity
    {
        [Required]
        public string DeparturePlace { get; set; }
        public DateTime DepartureTime{ get; set; }
        [Required]
        public string Destination { get; set; }
        public DateTime ArrivalTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Departure> Departures { get; set; }
    }
}
