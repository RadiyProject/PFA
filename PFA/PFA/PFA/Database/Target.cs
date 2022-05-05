using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class Target
    {
        public string name { get; set; }
        public float requiredMoney { get; set; }
        public float currentMoney { get; set; }
        public string totalText { get; set; }
        public DateTime deadLine { get; set; }
        public string deadLineText { get; set; }
        public string description { get; set; }
        public bool isOpened { get; set; }
        public bool isClosed { get; set; }
        public float colFirst { get; set; }
        public float colSecond { get; set; }
        public float colThird { get; set; }
        public float colFourth { get; set; }
        public DateTime lastAccessTime { get; set; }

        public Target(string name, DateTime deadLine)
        {
            this.name = name;
            this.deadLine = deadLine;
            deadLineText = deadLine.ToString("d");
            SetTotal();
        }
        public void AddMoney(float money)
        {
            currentMoney += money;
            SetTotal();
        }
        public void SetTotal()
        {
            totalText = currentMoney + "/" + requiredMoney + "р.";
        }
    }
}
