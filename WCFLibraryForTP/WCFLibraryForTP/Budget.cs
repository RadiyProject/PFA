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
    public class Budget
    {
        [DataMember][Key]
        public int id { get; set; }
        [DataMember]
        public float limit { get; set; }
        [DataMember]
        public bool hasLimit { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string targetsString { get; set; }
        [DataMember]
        public List<Target> targets = null;
        [DataMember]
        public List<Target> targetsN = null;

        public Budget() { }

        public Budget(BudgetS budgetS)
        {
            id = budgetS.budgetId;
            limit = budgetS.limit;
            if (budgetS.hasLimit == 0) hasLimit = false;
            else hasLimit = true;
            userId = budgetS.userId;
            targetsString = budgetS.targetsString;
        }
    }
}
