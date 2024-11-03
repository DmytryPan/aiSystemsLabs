using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public class Fact
    {
        public string id;
        public string factName { get; set; } // имя факта
        public bool isDeduced { get; set; } = false; // является ли он выведенным

        public Fact(string id, string factName)
        {
            this.id = id;
            this.factName = factName;
        }

    }
}
