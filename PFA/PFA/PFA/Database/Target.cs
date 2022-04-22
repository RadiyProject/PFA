using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class Target
    {
        public string name;
        public float requiredMoney;
        public float currentMoney;
        public DateTime deadLine;
        public string description;

        public Target(string name, DateTime deadLine)
        {
            this.name = name;
            this.deadLine = deadLine;
        }

        public void AddMoney(float money)
        {
            requiredMoney += money;
        }
    }
}
