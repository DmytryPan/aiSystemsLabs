﻿namespace Lab05
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
