using System;
using System.Collections.Generic;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Flight:Entity
    {
        public string DeparturePlace { get; set; }
        public DateTime DepartureTime{ get; set; }
        public string Destination { get; set; }
        public DateTime ArrivalTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Departure> Departures { get; set; }
    }
}
