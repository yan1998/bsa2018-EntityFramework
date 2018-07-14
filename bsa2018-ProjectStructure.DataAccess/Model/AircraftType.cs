
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class AircraftType:Entity
    {
        public Aircraft Aircraft { get; set; }
        [Range(minimum: 1, maximum: 1200)]
        public int Places { get; set; }
        [Range(minimum:0, maximum:1000000)]
        public float LoadCapacity { get; set; }
    }
}
