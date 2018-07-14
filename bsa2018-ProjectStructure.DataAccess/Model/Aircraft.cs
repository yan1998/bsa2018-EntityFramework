using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Aircraft:Entity
    {
        [MinLength(2),MaxLength(15)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan LifeSpan { get; set; }

        [Required]
        public int IdAircraftType { get; set; }
        public AircraftType AircraftType { get; set; }

        public ICollection<Departure> Departures { get; set; }
    }
}
