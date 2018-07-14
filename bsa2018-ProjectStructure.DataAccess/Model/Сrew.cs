using System.Collections.Generic;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Crew:Entity
    {
        public int IdPilot { get; set; }
        public Pilot Pilot { get; set; }

        public ICollection<StewardessCrew> StewardessCrews { get; set; }
        public ICollection<Departure> Departures { get; set; }
    }
}
