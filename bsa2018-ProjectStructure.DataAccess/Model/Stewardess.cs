using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class Stewardess:Entity
    {
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        [MinLength(2), MaxLength(20)]
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<StewardessCrew> StewardessCrews { get; set; }
    }
}
