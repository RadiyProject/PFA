using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    [DataContract]
    public class Target
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public float requiredMoney { get; set; }
        [DataMember]
        public float currentMoney { get; set; }
        [DataMember]
        public string totalText { get; set; }
        [DataMember]
        public DateTime deadLine { get; set; }
        [DataMember]
        public string deadLineText { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public bool isOpened { get; set; }
        [DataMember]
        public bool isClosed { get; set; }
        [DataMember]
        public float colFirst { get; set; }
        [DataMember]
        public float colSecond { get; set; }
        [DataMember]
        public float colThird { get; set; }
        [DataMember]
        public float colFourth { get; set; }
        [DataMember]
        public DateTime lastAccessTime { get; set; }
        [DataMember]
        public int budgetId { get; set; }
        [DataMember]
        public int id { get; set; }

        public Target() { }

        public Target(TargetS target)
        {
            name = target.name;
            requiredMoney = target.requiredMoney;
            currentMoney = target.currentMoney;
            totalText = target.totalText;
            DateTime dateTime;
            DateTime.TryParse(target.deadLineText, out dateTime);
            deadLine = dateTime;
            deadLineText = target.deadLineText;
            description = target.description;
            if (target.isOpened == 1) isOpened = true;
            else isOpened = false;
            if (target.isClosed == 1) isClosed = true;
            else isClosed = false;
            colFirst = target.colFirst;
            colSecond = target.colSecond;
            colThird = target.colThird;
            colFourth = target.colFourth;
            DateTime.TryParse(target.lastAccessTime, out dateTime);
            lastAccessTime = dateTime;
            budgetId = target.budgetId;
            id = target.id;
        }
    }
}
