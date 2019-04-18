using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessor
{
   public class ThreatShort : Threat
    {
        public override string ToString()
        {
            return
               "Идентификатор УБИ: " + this.ID
               + "Наименование угрозы: " + this.Name
               + "\nИсточник угрозы: " + SourceOfThreat;
        }
    }
}
