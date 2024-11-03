using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public class Rule
    {

        internal List<Fact> Conditions { get; set; } 
        internal Fact Conclusion { get; set; }

    }
}
