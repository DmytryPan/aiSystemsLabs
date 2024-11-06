using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public class Fact
    {
        public string ID; // идентификатор факта
        public string FactName { get; set; } // имя факта

        public Fact(string id, string factName)
        {
            this.ID = id;
            this.FactName = factName;
        }

    }
}
