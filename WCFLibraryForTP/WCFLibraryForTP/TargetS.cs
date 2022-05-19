using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    [DataContract]
    public class TargetS
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
        public string deadLineText { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int isOpened { get; set; }
        [DataMember]
        public int isClosed { get; set; }
        [DataMember]
        public float colFirst { get; set; }
        [DataMember]
        public float colSecond { get; set; }
        [DataMember]
        public float colThird { get; set; }
        [DataMember]
        public float colFourth { get; set; }
        [DataMember]
        public string lastAccessTime { get; set; }
        [DataMember]
        public int budgetId { get; set; }
        [DataMember] [Key]
        public int id { get; set; }

        public TargetS() { }

        public TargetS(Target target)
        {
            name = target.name;
            requiredMoney = target.requiredMoney;
            currentMoney = target.currentMoney;
            totalText = target.totalText;
            deadLineText = target.deadLineText;
            description = target.description;
            if (target.isOpened == true) isOpened = 1;
            else isOpened = 0;
            if (target.isClosed == true) isClosed = 1;
            else isClosed = 0;
            colFirst = target.colFirst;
            colSecond = target.colSecond;
            colThird = target.colThird;
            colFourth = target.colFourth;
            lastAccessTime = target.lastAccessTime.ToString("d");
            budgetId = target.budgetId;
        }

    }
}
