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
    public class BudgetS
    {
        [DataMember]
        [Key]
        public int budgetId { get; set; }
        [DataMember]
        public float limit { get; set; }
        [DataMember]
        public int hasLimit { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string targetsString { get; set; }


        public BudgetS() { }

        public BudgetS(float limit, int hasLimit, string userId, string targetsString)
        {
            this.limit = limit;
            this.hasLimit = hasLimit;
            this.userId = userId;
            this.targetsString = targetsString;
        }

        public BudgetS(Budget b)
        {
            budgetId = b.id;
            limit = b.limit;
            if (b.hasLimit == false) hasLimit = 0;
            else hasLimit = 1;
            userId = b.userId;
            targetsString = b.targetsString;
        }


    }
}
