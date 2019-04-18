using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataProcessor
{
    public class Threat
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceOfThreat { get; set; }
        public string ObjectOfImpact { get; set; }
        public string BreachOfСonfidentiality { get; set; }
        public string BreachOfintegrity { get; set; }
        public string BreachOfAccess { get; set; }
        public string DateInclude { get; set; }
        public string DateChange { get; set; }

        public Threat() { }

        public Threat(int ID, string Name, string Description, string SourceOfThreat, string ObjectOfImpact, string BreachOfСonfidentiality, string BreachOfintegrity, string BreachOfAccess, string DateInclude, string DateChange)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.SourceOfThreat = SourceOfThreat;
            this.ObjectOfImpact = ObjectOfImpact;
            this.BreachOfСonfidentiality = BreachOfСonfidentiality;
            this.BreachOfintegrity = BreachOfintegrity;
            this.BreachOfAccess = BreachOfAccess;
            this.DateInclude = DateInclude;
            this.DateChange = DateChange;
        }

        public override string ToString()
        {
            return
                "Идентификатор УБИ: " + this.ID
                + " Наименование угрозы: " + this.Name
                + "\nОписание: " + Description
                + "\nИсточник угрозы: " + SourceOfThreat
                + "\nобъект воздействия: " + ObjectOfImpact
                + "\nНарушение конфиденциальности: " + BreachOfСonfidentiality
                + "\nНарушение целостности: " + BreachOfintegrity
                + "\nНарушение доступности: " + BreachOfAccess
                + "\nДата включения угрозы в БнД УБИ " + DateInclude
                + "\nДата последнего изменения данных: " + DateChange;


        }
    }
}

