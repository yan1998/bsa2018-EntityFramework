using System;
using System.Collections.Generic;
using System.Text;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class StewardessCrew
    {
        public int IdStewardess { get; set }
        public Stewardess Stewardess { get; set; }

        public int IdCrew { get; set; }
        public Crew Crew { get; set; }
    }
}
