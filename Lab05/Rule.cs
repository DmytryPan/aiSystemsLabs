using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lab05
{
    public class Rule
    {
        //Факты-условия
        internal List<Fact> Conditions { get; set; } 
       //Факты-заключения
        internal Fact Conclusion { get; set; }
      //Словесное описание правила
        public string Description { get; set; }
        //Описание правила в ID
        public string DescriptionID { get; set; }

    }
}
